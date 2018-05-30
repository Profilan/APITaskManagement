using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Profilan.ApiResource.Configuration
{
    public class Resource : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("driver", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string driver
        {
            get { return (string)base["driver"]; }
            set { base["driver"] = value; }
        }

    }
}
