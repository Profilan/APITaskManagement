using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Config
{
    public class QueueConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public QueueElementCollection Queues
        {
            get
            {
                return (QueueElementCollection)base[""];
            }
        }
    }
}
