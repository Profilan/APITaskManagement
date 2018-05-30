using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Monitoring.Interfaces;
using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Monitoring.ApplicationEvents
{
    public class ErrorDetectedEvent : IApplicationEvent
    {
        public IMonitor Monitor { get; private set; }

        public DateTime DateTimeEventOccurred { get; private set; }

        public string EventType
        {
            get
            {
                return "ErrorDetectedEvent";
            }
        }

        public ErrorDetectedEvent(IMonitor monitor) : this()
        {
            Monitor = monitor;
            
        }

        public ErrorDetectedEvent()
        {
            DateTimeEventOccurred = DateTime.Now;
        }
    }
}
