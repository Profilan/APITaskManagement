using APITaskManagement.Logic.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers.Events
{
    public class LastRunChangedEvent : IDomainEvent
    {
        public string LastRunResult { get; }
        public DateTime LastRunTime { get; }
        public string LastRunDetails { get; }
        public bool Enabled { get; }
        public bool Active { get; }

        public DateTime DateTimeEventOccurred { get; }
        public Guid Id { get; }

        public LastRunChangedEvent(string lastRunResult, DateTime lastRunTime, string lastRunDetails, bool enabled, bool active) : this()
        {
            LastRunResult = lastRunResult;
            LastRunTime = lastRunTime;
            LastRunDetails = lastRunDetails;
            Enabled = enabled;
            Active = active;
        }

        public LastRunChangedEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}
