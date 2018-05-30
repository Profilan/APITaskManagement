using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Service.Config
{
    public class SchedulerConfig : ConfigurationSection
    {
        private static SchedulerConfig _schedulerConfig = (SchedulerConfig)ConfigurationManager.GetSection("schedulerSection");
        public static SchedulerConfig Settings { get { return _schedulerConfig; } }

        [ConfigurationProperty("signalr")]
        public SchedulerElement SignalR
        {
            get
            {
                return (SchedulerElement)base["signalr"];
            }
        }
    }
}
