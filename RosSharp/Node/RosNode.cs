﻿#region License Terms

// ================================================================================
// RosSharp
// 
// Software License Agreement (BSD License)
// 
// Copyright (C) 2012 zoetrope
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// ================================================================================

#endregion

using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using RosSharp.Master;
using RosSharp.Message;
using RosSharp.Parameter;
using RosSharp.Service;
using RosSharp.Slave;
using RosSharp.Topic;
using RosSharp.Transport;
using RosSharp.Utility;
using RosSharp.roscpp;
using RosSharp.rosgraph_msgs;

namespace RosSharp.Node
{
    /// <summary>
    /// ROS Node
    /// </summary>
    public class RosNode : INode
    {
        private bool _disposed;
        private readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private readonly MasterClient _masterClient;
        private readonly ParameterServerClient _parameterServerClient;
        private readonly Dictionary<string, IParameter> _parameters = new Dictionary<string, IParameter>();
        private readonly ServiceProxyFactory _serviceProxyFactory;
        private readonly Dictionary<string, IService> _serviceServers = new Dictionary<string, IService>();
        private readonly Dictionary<string, IServiceProxy> _serviceProxies = new Dictionary<string, IServiceProxy>();

        private readonly SlaveServer _slaveServer;
        private readonly TopicContainer _topicContainer;
        internal event Action<RosNode> Disposing;
        internal byte LogLevel { get; private set; }

        public RosNode(string nodeId)
        {
            LogLevel = Log.INFO;

            _logger.InfoFormat("Create Node: {0}", nodeId);

            NodeId = nodeId;

            _masterClient = new MasterClient(Ros.MasterUri);
            _parameterServerClient = new ParameterServerClient(Ros.MasterUri);

            _serviceProxyFactory = new ServiceProxyFactory(NodeId);

            _topicContainer = new TopicContainer();
            _slaveServer = new SlaveServer(NodeId, 0, _topicContainer);

            _slaveServer.ParameterUpdated += SlaveServerOnParameterUpdated;
        }

        public string NodeId { get; private set; }

        internal Publisher<Log> LogPubliser { get; private set; }

        internal Task InitializeAsync(bool enableLogger)
        {
            if (enableLogger)
            {
                var t1 = CreatePublisherAsync<Log>("/rosout").ContinueWith(t => LogPubliser = t.Result);

                var t2 = RegisterServiceAsync(NodeId + "/get_loggers", new GetLoggers(GetLoggers));
                var t3 = RegisterServiceAsync(NodeId + "/set_logger_level", new SetLoggerLevel(SetLoggerLevel));

                return Task.Factory.StartNew(() => Task.WaitAll(new Task[] {t1, t2, t3}));
            }
            else
            {
                return Task.Factory.StartNew(() => { });
            }

        }


        internal SetLoggerLevel.Response SetLoggerLevel(SetLoggerLevel.Request request)
        {
            if (request.logger == "RosSharp")
            {
                byte level;
                if (LogLevelExtensions.TryParse(request.level, out level))
                {
                    LogLevel = level;
                }
            }

            return new SetLoggerLevel.Response();
        }

        internal GetLoggers.Response GetLoggers(GetLoggers.Request request)
        {
            return new GetLoggers.Response()
            {
                loggers = new List<Logger>()
                {
                    new Logger() {name = "RosSharp", level = LogLevel.ToLogLevelString()}
                }
            };
        }

        #region INode Members

        public Task<Parameter<T>> CreateParameterAsync<T>(string paramName)
        {
            if (_disposed) throw new ObjectDisposedException("RosNode");

            if (_parameters.ContainsKey(paramName))
            {
                throw new InvalidOperationException(paramName + " is already created.");
            }

            var param = new Parameter<T>(NodeId, paramName, _slaveServer.SlaveUri, _parameterServerClient);

            _parameters.Add(paramName, param);

            var tcs = new TaskCompletionSource<Parameter<T>>();

            param.InitializeAsync().ContinueWith(task =>
            {
                if(task.Status == TaskStatus.RanToCompletion)
                {
                    tcs.SetResult(param);
                }
                else if (task.Status == TaskStatus.Faulted)
                {
                    tcs.SetException(task.Exception.InnerException);
                    _logger.Error("Initialize Parameter: Failure", task.Exception.InnerException);
                }
            });

            return tcs.Task;
        }

        public void Dispose()
        {
            if (_disposed) throw new ObjectDisposedException("RosNode");
            //TODO: すべてを終了させる。ロックが必要？

            var handler = Disposing;
            if (handler != null)
            {
                handler(this);
            }
            Disposing = null;

            //終了待ち

            _slaveServer.Dispose();

            _disposed = true;
        }

        public Task<Subscriber<TMessage>> CreateSubscriberAsync<TMessage>(string topicName, bool nodelay = true)
            where TMessage : IMessage, new()
        {
            if (_disposed) throw new ObjectDisposedException("RosNode");

            if (_topicContainer.HasSubscriber(topicName))
            {
                throw new InvalidOperationException(topicName + " is already created.");
            }

            _logger.InfoFormat("Create Subscriber: {0}", topicName);

            var subscriber = new Subscriber<TMessage>(topicName, NodeId, nodelay);
            _topicContainer.AddSubscriber(subscriber);
            subscriber.Disposing += DisposeSubscriber;

            var tcs = new TaskCompletionSource<Subscriber<TMessage>>();

            _logger.Debug("RegisterSubscriber");
            _masterClient
                .RegisterSubscriberAsync(NodeId, topicName, subscriber.MessageType, _slaveServer.SlaveUri)
                .ContinueWith(task =>
                {
                    _logger.Debug("Registered Subscriber");
                    if (task.IsFaulted)
                    {
                        tcs.SetException(task.Exception.InnerException);
                        _logger.Error("RegisterSubscriber: Failure", task.Exception.InnerException);
                    }
                    else if (task.IsCanceled)
                    {
                        tcs.SetCanceled();
                    }
                    else
                    {
                        ((ISubscriber) subscriber).UpdatePublishers(task.Result);
                        tcs.SetResult(subscriber);
                    }
                });

            return tcs.Task;
        }

        public Task<Publisher<TMessage>> CreatePublisherAsync<TMessage>(string topicName, bool latching = false)
            where TMessage : IMessage, new()
        {
            if (_disposed) throw new ObjectDisposedException("RosNode");

            if (_topicContainer.HasPublisher(topicName))
            {
                throw new InvalidOperationException(topicName + " is already created.");
            }

            _logger.InfoFormat("Create Publisher: {0}", topicName);

            var publisher = new Publisher<TMessage>(topicName, NodeId, latching);
            _topicContainer.AddPublisher(publisher);
            publisher.Disposing += DisposePublisher;

            var tcpRosListener = new TcpRosListener(0);
            _slaveServer.AddListener(topicName, tcpRosListener);

            tcpRosListener.AcceptAsync()
                .Do(_ => _logger.Debug("Accepted for Publisher"))
                .Subscribe(socket => publisher.AddTopic(socket),
                           ex => _logger.Error("Accept Error", ex));

            var tcs = new TaskCompletionSource<Publisher<TMessage>>();

            _logger.Debug("RegisterPublisher");
            _masterClient
                .RegisterPublisherAsync(NodeId, topicName, publisher.MessageType, _slaveServer.SlaveUri)
                .ContinueWith(task =>
                {
                    _logger.Debug("Registered Publisher");

                    if (task.IsFaulted)
                    {
                        tcs.SetException(task.Exception.InnerException);
                        _logger.Error("RegisterPublisher: Failure", task.Exception.InnerException);
                    }
                    else if (task.IsCanceled)
                    {
                        tcs.SetCanceled();
                    }
                    else
                    {
                        publisher.UpdateSubscribers(task.Result);
                        tcs.SetResult(publisher);
                    }
                });

            return tcs.Task;
        }

        public Task<TService> CreateProxyAsync<TService>(string serviceName)
            where TService : IService, new()
        {
            if (_disposed) throw new ObjectDisposedException("RosNode");
            if (_serviceProxies.ContainsKey(serviceName))
            {
                throw new InvalidOperationException(serviceName + " is already created.");
            }

            _logger.InfoFormat("Create ServiceProxy: {0}", serviceName);

            var tcs = new TaskCompletionSource<TService>();

            _masterClient
                .LookupServiceAsync(NodeId, serviceName)
                .ContinueWith(lookupTask =>
                {
                    _logger.Debug("Registered Subscriber");

                    if (lookupTask.Status == TaskStatus.RanToCompletion)
                    {
                        _serviceProxyFactory.CreateAsync<TService>(serviceName, lookupTask.Result)
                            .ContinueWith(createTask =>
                            {
                                if (createTask.Status == TaskStatus.RanToCompletion)
                                {
                                    var proxy = createTask.Result;
                                    _serviceProxies.Add(serviceName, proxy);
                                    tcs.SetResult(proxy.Service);
                                }
                                else if (createTask.Status == TaskStatus.Faulted)
                                {
                                    tcs.SetException(createTask.Exception.InnerException);
                                }
                            });
                    }
                    else if (lookupTask.Status == TaskStatus.Faulted)
                    {
                        tcs.SetException(lookupTask.Exception.InnerException);
                        _logger.Error("RegisterSubscriber: Failure", lookupTask.Exception.InnerException);
                    }
                });

            return tcs.Task;
        }

        public Task<IDisposable> RegisterServiceAsync<TService>(string serviceName, TService service)
            where TService : IService, new()
        {
            if (_disposed) throw new ObjectDisposedException("RosNode");
            if (_serviceServers.ContainsKey(serviceName))
            {
                throw new InvalidOperationException(serviceName + " is already registered.");
            }

            _logger.InfoFormat("Create ServiceServer: {0}", serviceName);

            var serviceServer = new ServiceServer<TService>(NodeId);
            var disposable = serviceServer.StartService(serviceName, service);
            _serviceServers.Add(serviceName, service);

            var cd = new CompositeDisposable(serviceServer, disposable);

            var serviceUri = new Uri("rosrpc://" + Ros.HostName + ":" + serviceServer.EndPoint.Port);

            var tcs = new TaskCompletionSource<IDisposable>();

            _masterClient
                .RegisterServiceAsync(NodeId, serviceName, serviceUri, _slaveServer.SlaveUri)
                .ContinueWith(registerTask =>
                {
                    if (registerTask.Status == TaskStatus.RanToCompletion)
                    {
                        tcs.SetResult(cd);
                    }
                    else if (registerTask.Status == TaskStatus.Faulted)
                    {
                        tcs.SetException(registerTask.Exception.InnerException);
                    }
                });
            return tcs.Task;
        }

        #endregion

        private void DisposeSubscriber(ISubscriber subscriber)
        {
            var topicName = subscriber.TopicName;
            _logger.Debug(m => m("Disposing Subscriber[{0}]", topicName));

            _masterClient
                .UnregisterSubscriberAsync(NodeId, topicName, _slaveServer.SlaveUri)
                .ContinueWith(task =>
                {
                    if(task.IsFaulted)
                    {
                        _logger.Error("UnregisterSubscriber: Failure", task.Exception.InnerException);
                    }
                    _topicContainer.RemoveSubscriber(topicName);
                    _logger.Debug(m => m("UnregisterSubscriber: [{0}]", topicName));
                });
        }

        private void DisposePublisher(IPublisher publisher)
        {
            var topicName = publisher.TopicName;
            _logger.Debug(m => m("Disposing Publisher[{0}]", topicName));

            _masterClient
                .UnregisterPublisherAsync(NodeId, topicName, _slaveServer.SlaveUri)
                .ContinueWith(task =>
                {
                    if(task.IsFaulted)
                    {
                        _logger.Error("UnregisterPublisher: Failure", task.Exception.InnerException);
                    }
                    _topicContainer.RemovePublisher(topicName);
                    _logger.Debug(m => m("DispUnregisterPublisher: [{0}]", topicName));
                });
        }

        internal Task RemoveServiceAsync(string serviceName)
        {
            return _masterClient
                .UnregisterServiceAsync(NodeId, serviceName, _slaveServer.SlaveUri)
                .ContinueWith(_ => _serviceServers.Remove(serviceName));
        }

        private void SlaveServerOnParameterUpdated(string key, object value)
        {
            if (!_parameters.ContainsKey(key))
            {
                return;
            }

            var param = _parameters[key];
            param.Update(value);
        }
    }
}