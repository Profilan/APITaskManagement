using APITaskManagement.Logic.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Schedulers.ApplicationEvents
{
    public class TaskStartedEvent : IApplicationEvent
    {
        public Task StartedTask { get; private set; }

        public DateTime DateTimeEventOccurred { get; private set; }

        public string EventType
        {
            get
            {
                return "TaskStartedEvent";
            }
        }

        public TaskStartedEvent(Task task) : this()
        {
            StartedTask = task;
            
        }

        public TaskStartedEvent()
        {
            DateTimeEventOccurred = DateTime.Now;
        }
    }
}
