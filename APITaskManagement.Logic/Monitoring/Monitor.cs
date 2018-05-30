using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Monitoring.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Monitoring
{
    public class Monitor : Entity<Guid>, IMonitor
    {
        public virtual string Name { get; set; }

        public virtual bool Enabled { get; set; }
 
        public virtual ISet<Messenger> Messengers { get; set; }

        public Monitor() : base(Guid.NewGuid())
        {
            Messengers = new HashSet<Messenger>();
        }

        public virtual void Run(ISet<Messenger> messengers)
        {

        }
    }
}
