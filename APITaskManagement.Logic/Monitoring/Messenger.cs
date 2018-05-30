using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Monitoring.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Monitoring
{
    public class Messenger : Entity<Guid>, IMessenger
    {
        public virtual string Name { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual ISet<Monitor> Monitors { get; set; }

        public Messenger() : base(Guid.NewGuid())
        {
            Enabled = false;

            Monitors = new HashSet<Monitor>();
        }

        public virtual void Send(string subject, string body)
        {

        }
    }
}
