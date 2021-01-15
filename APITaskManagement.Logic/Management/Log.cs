using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Schedulers;
using System;

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
        public virtual double Duration { get; set; }

        public virtual User User { get; set; }

        public virtual Task Task { get; set; }

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
             bool acknowledged,
             User user,
             Task task)
        {
            TimeStamp = timeStamp;
            Priority = priority;
            Message = message;
            PriorityName = priorityName;
            Url = url;
            Detail = detail;
            Acknowledged = acknowledged;
            User = user;
            Task = task;
        }
    }
}
