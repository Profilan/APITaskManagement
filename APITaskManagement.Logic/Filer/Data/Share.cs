using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace APITaskManagement.Logic.Filer.Data
{
    public class Share : Entity<int>
    {
        public virtual string Name { get; set; }
        public virtual string UNCPath { get; set; }

        public virtual Interval InactivityTimeout { get; set; }
        public virtual bool MonitorInactivity { get; set; }

        public virtual ISet<Task> Tasks { get; set; }

        public Share()
        {
            Tasks = new HashSet<Task>();
        }
    }
}
