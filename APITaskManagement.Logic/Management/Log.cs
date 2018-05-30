using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Management
{
    public class Log : Entity<int>
    {
        public virtual DateTime TimeStamp { get; protected set; }
        public virtual int Priority { get; protected set; }
        public virtual string Message { get; protected set; }
        public virtual string PriorityName { get; protected set; }
        public virtual string Url { get; protected set; }
        public virtual string Detail { get; protected set; }
        public virtual bool Acknowledged { get; set; }

        // Needed for NHibernate
        public Log()
        {

        }

        public Log(DateTime timeStamp,
            int priority,
            string message,
            string priorityName,
            string url,
            string detail,
             bool acknowledged)
        {
            TimeStamp = timeStamp;
            Priority = priority;
            Message = message;
            PriorityName = priorityName;
            Url = url;
            Detail = detail;
            Acknowledged = acknowledged;
        }

        // requestId, targetId, response, properties, referenceId
    }
}
