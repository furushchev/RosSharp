//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.261
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace RosSharp
{
    
    
    /// <summary>
    /// The ConfigurationSection Configuration Section.
    /// </summary>
    public partial class ConfigurationSection : global::System.Configuration.ConfigurationSection
    {
        
        #region Singleton Instance
        /// <summary>
        /// The XML name of the ConfigurationSection Configuration Section.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        internal const string ConfigurationSectionSectionName = "rossharp";
        
        /// <summary>
        /// Gets the ConfigurationSection instance.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        public static global::RosSharp.ConfigurationSection Instance
        {
            get
            {
                return ((global::RosSharp.ConfigurationSection)(global::System.Configuration.ConfigurationManager.GetSection(global::RosSharp.ConfigurationSection.ConfigurationSectionSectionName)));
            }
        }
        #endregion
        
        #region Xmlns Property
        /// <summary>
        /// The XML name of the <see cref="Xmlns"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        internal const string XmlnsPropertyName = "xmlns";
        
        /// <summary>
        /// Gets the XML namespace of this Configuration Section.
        /// </summary>
        /// <remarks>
        /// This property makes sure that if the configuration file contains the XML namespace,
        /// the parser doesn't throw an exception because it encounters the unknown "xmlns" attribute.
        /// </remarks>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::RosSharp.ConfigurationSection.XmlnsPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public string Xmlns
        {
            get
            {
                return ((string)(base[global::RosSharp.ConfigurationSection.XmlnsPropertyName]));
            }
        }
        #endregion
        
        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
        
        #region MasterUri Property
        /// <summary>
        /// The XML name of the <see cref="MasterUri"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        internal const string MasterUriPropertyName = "ROS_MASTER_URI";
        
        /// <summary>
        /// Gets or sets the MasterUri.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        [global::System.ComponentModel.DescriptionAttribute("The MasterUri.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::RosSharp.ConfigurationSection.MasterUriPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public global::RosSharp.RosMasterUri MasterUri
        {
            get
            {
                return ((global::RosSharp.RosMasterUri)(base[global::RosSharp.ConfigurationSection.MasterUriPropertyName]));
            }
            set
            {
                base[global::RosSharp.ConfigurationSection.MasterUriPropertyName] = value;
            }
        }
        #endregion
        
        #region HostName Property
        /// <summary>
        /// The XML name of the <see cref="HostName"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        internal const string HostNamePropertyName = "ROS_HOSTNAME";
        
        /// <summary>
        /// Gets or sets the HostName.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        [global::System.ComponentModel.DescriptionAttribute("The HostName.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::RosSharp.ConfigurationSection.HostNamePropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public global::RosSharp.RosHostName HostName
        {
            get
            {
                return ((global::RosSharp.RosHostName)(base[global::RosSharp.ConfigurationSection.HostNamePropertyName]));
            }
            set
            {
                base[global::RosSharp.ConfigurationSection.HostNamePropertyName] = value;
            }
        }
        #endregion
        
        #region XmlRpcTimeout Property
        /// <summary>
        /// The XML name of the <see cref="XmlRpcTimeout"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        internal const string XmlRpcTimeoutPropertyName = "XMLRPC_TIMEOUT";
        
        /// <summary>
        /// Gets or sets the XmlRpcTimeout.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        [global::System.ComponentModel.DescriptionAttribute("The XmlRpcTimeout.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::RosSharp.ConfigurationSection.XmlRpcTimeoutPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public global::RosSharp.XmlRpcTimeout XmlRpcTimeout
        {
            get
            {
                return ((global::RosSharp.XmlRpcTimeout)(base[global::RosSharp.ConfigurationSection.XmlRpcTimeoutPropertyName]));
            }
            set
            {
                base[global::RosSharp.ConfigurationSection.XmlRpcTimeoutPropertyName] = value;
            }
        }
        #endregion
        
        #region SocketTimeout Property
        /// <summary>
        /// The XML name of the <see cref="SocketTimeout"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        internal const string SocketTimeoutPropertyName = "SOCKET_TIMEOUT";
        
        /// <summary>
        /// Gets or sets the SocketTimeout.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        [global::System.ComponentModel.DescriptionAttribute("The SocketTimeout.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::RosSharp.ConfigurationSection.SocketTimeoutPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public global::RosSharp.SocketTimeout SocketTimeout
        {
            get
            {
                return ((global::RosSharp.SocketTimeout)(base[global::RosSharp.ConfigurationSection.SocketTimeoutPropertyName]));
            }
            set
            {
                base[global::RosSharp.ConfigurationSection.SocketTimeoutPropertyName] = value;
            }
        }
        #endregion
    }
}
namespace RosSharp
{
    
    
    /// <summary>
    /// The RosHostName Configuration Element.
    /// </summary>
    public partial class RosHostName : global::System.Configuration.ConfigurationElement
    {
        
        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
        
        #region Value Property
        /// <summary>
        /// The XML name of the <see cref="Value"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        internal const string ValuePropertyName = "value";
        
        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        [global::System.ComponentModel.DescriptionAttribute("The Value.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::RosSharp.RosHostName.ValuePropertyName, IsRequired=true, IsKey=false, IsDefaultCollection=false)]
        public string Value
        {
            get
            {
                return ((string)(base[global::RosSharp.RosHostName.ValuePropertyName]));
            }
            set
            {
                base[global::RosSharp.RosHostName.ValuePropertyName] = value;
            }
        }
        #endregion
    }
}
namespace RosSharp
{
    
    
    /// <summary>
    /// The RosMasterUri Configuration Element.
    /// </summary>
    public partial class RosMasterUri : global::System.Configuration.ConfigurationElement
    {
        
        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
        
        #region Value Property
        /// <summary>
        /// The XML name of the <see cref="Value"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        internal const string ValuePropertyName = "value";
        
        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        [global::System.ComponentModel.DescriptionAttribute("The Value.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::RosSharp.RosMasterUri.ValuePropertyName, IsRequired=true, IsKey=false, IsDefaultCollection=false)]
        public string Value
        {
            get
            {
                return ((string)(base[global::RosSharp.RosMasterUri.ValuePropertyName]));
            }
            set
            {
                base[global::RosSharp.RosMasterUri.ValuePropertyName] = value;
            }
        }
        #endregion
    }
}
namespace RosSharp
{
    
    
    /// <summary>
    /// The XmlRpcTimeout Configuration Element.
    /// </summary>
    public partial class XmlRpcTimeout : global::System.Configuration.ConfigurationElement
    {
        
        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
        
        #region Value Property
        /// <summary>
        /// The XML name of the <see cref="Value"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        internal const string ValuePropertyName = "value";
        
        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        [global::System.ComponentModel.DescriptionAttribute("The Value.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::RosSharp.XmlRpcTimeout.ValuePropertyName, IsRequired=true, IsKey=false, IsDefaultCollection=false, DefaultValue=3000)]
        public int Value
        {
            get
            {
                return ((int)(base[global::RosSharp.XmlRpcTimeout.ValuePropertyName]));
            }
            set
            {
                base[global::RosSharp.XmlRpcTimeout.ValuePropertyName] = value;
            }
        }
        #endregion
    }
}
namespace RosSharp
{
    
    
    /// <summary>
    /// The SocketTimeout Configuration Element.
    /// </summary>
    public partial class SocketTimeout : global::System.Configuration.ConfigurationElement
    {
        
        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
        
        #region Value Property
        /// <summary>
        /// The XML name of the <see cref="Value"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        internal const string ValuePropertyName = "value";
        
        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.0")]
        [global::System.ComponentModel.DescriptionAttribute("The Value.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::RosSharp.SocketTimeout.ValuePropertyName, IsRequired=true, IsKey=false, IsDefaultCollection=false, DefaultValue=3000)]
        public int Value
        {
            get
            {
                return ((int)(base[global::RosSharp.SocketTimeout.ValuePropertyName]));
            }
            set
            {
                base[global::RosSharp.SocketTimeout.ValuePropertyName] = value;
            }
        }
        #endregion
    }
}
