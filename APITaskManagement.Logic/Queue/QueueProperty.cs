using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Queue
{
    public class QueueProperty : Entity<int>
    {
        public virtual string Name { get; protected set; }
        public virtual string Value { get; protected set; }
        public virtual Queue Queue { get; protected set; }

        public QueueProperty(int id) : base(id)
        {

        }

        public QueueProperty()
        {

        }
    }
}
