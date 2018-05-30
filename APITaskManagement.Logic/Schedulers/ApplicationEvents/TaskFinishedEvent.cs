using APITaskManagement.Logic.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Schedulers.ApplicationEvents
{
    public class TaskFinishedEvent : IApplicationEvent
    {
        public Task FinishedTask { get; private set; }

        public DateTime DateTimeEventOccurred { get; private set; }

        public string EventType
        {
            get
            {
                return "TaskFinishedEvent";
            }
        }

        public TaskFinishedEvent(Task task) : this()
        {
            FinishedTask = task;
            
        }

        public TaskFinishedEvent()
        {
            DateTimeEventOccurred = DateTime.Now;
        }
    }
}
