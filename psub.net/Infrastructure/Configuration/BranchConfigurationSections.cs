using System.Configuration;

namespace Infrastructure.Configuration
{
    public class BranchesConfigSection : ConfigurationSection 
    {
        [ConfigurationProperty("clients", IsRequired = false, IsDefaultCollection = false)]
        public BranchClientCollection Client
        {
            get { return (BranchClientCollection)this["clients"]; }
            set { this["clients"] = value; }
        }
        [ConfigurationProperty("servers", IsRequired = false, IsDefaultCollection = false)]
        public BranchServerCollection Server
        {
            get { return (BranchServerCollection)this["servers"]; }
            set { this["servers"] = value; }
        }
    }

    #region collections

    public class BranchServerCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new BranchServerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BranchServerElement)element).Name;
        }

        [ConfigurationProperty("current", IsRequired = true)]
        public string Current
        {
            get
            {
                return (string)base["current"];
            }
        }
    }

    public class BranchClientCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new BranchClientElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BranchClientElement)element).Name;
        }
    }

    #endregion

    #region elements

    public class BranchBaseElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("salt", IsKey = false, IsRequired = true)]
        public string Salt
        {
            get { return (string)base["salt"]; }
            set { base["salt"] = value; }
        }

        [ConfigurationProperty("encryptionKey", IsKey = false, IsRequired = true)]
        public string EncryptionKey
        {
            get { return (string)base["encryptionKey"]; }
            set { base["encryptionKey"] = value; }
        }

        [ConfigurationProperty("encryptionVektor", IsKey = false, IsRequired = true)]
        public string EncryptionVektor
        {
            get { return (string)base["encryptionVektor"]; }
            set { base["encryptionVektor"] = value; }
        }
    }

    public class BranchServerElement : BranchBaseElement
    {
        [ConfigurationProperty("endpoint", IsKey = false, IsRequired = true)]
        public string Endpoint
        {
            get { return (string)base["endpoint"]; }
            set { base["endpoint"] = value; }
        }

        [ConfigurationProperty("title", IsKey = false, IsRequired = true)]
        public string Title
        {
            get { return (string)base["title"]; }
            set { base["title"] = value; }
        }
    }

    public class BranchClientElement : BranchBaseElement
    {
    }

    #endregion
}
