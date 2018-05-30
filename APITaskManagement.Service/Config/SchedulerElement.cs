using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Service.Config
{
    public class SchedulerElement : ConfigurationElement
    {
        [ConfigurationProperty("hubConnectionUrl")]
        public string HubConnectionUrl { get { return (string)base["hubConnectionUrl"];  } }
    }
}
