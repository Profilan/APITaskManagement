using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Schedulers.Events;
using Microsoft.AspNet.SignalR;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Service
{
    public class LastRunChangedEventHandler : IHandleMessages<LastRunChangedEvent>
    {
        static ILog log = LogManager.GetLogger<LastRunChangedEventHandler>();

        public Task Handle(LastRunChangedEvent message, IMessageHandlerContext context)
        {
            /*
           var hubContext = GlobalHost.ConnectionManager.GetHubContext<TaskSchedulerHub>();
           hubContext.Clients.All.updateTaskStatus(domainEvent.Id, domainEvent.Active, domainEvent.LastRunTime.ToString("dd-MM-yyyy HH:mm:ss"), domainEvent.LastRunResult, domainEvent.Enabled);
           */

            log.Info($"Subscriber has received LastRunChangedEvent with TaskId {message.Id}");
            return Task.CompletedTask;
        }
    }
}
