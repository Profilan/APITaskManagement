using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Config
{
    public class QueueElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name { get { return (string)this["name"];  } }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type { get { return (string)this["type"]; } }

        [ConfigurationProperty("properties", IsDefaultCollection = false)]
        public PropertyElementCollection PropertyCollection
        {
            get
            {
                return (PropertyElementCollection)base["properties"];
            }
        }

    }
}
