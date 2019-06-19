using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Schedulers.ApplicationEvents;
using APITaskManagement.Logic.Schedulers.Repositories;
using APITaskManagement.Service.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Service.Handlers
{
    public class TaskStartedHandler : IHandle<TaskStartedEvent>
    {
        private readonly TaskRepository _taskRepository = new TaskRepository();

        public void Handle(TaskStartedEvent args)
        {
            args.StartedTask.Active = true;
            _taskRepository.Update(args.StartedTask);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TaskSchedulerHub>();
            hubContext.Clients.All.updateTask(args.StartedTask.Id.ToString(), args.StartedTask.Title, args.StartedTask.Active, args.StartedTask.LastRunTime.ToString("dd-MM-yyyy HH:mm:ss"), args.StartedTask.LastRunResult, args.StartedTask.Enabled);

            EventLog eventLog = new EventLog();
            eventLog.Source = "API Task Scheduler";
            eventLog.Log = "API Task Management";

            eventLog.WriteEntry("Task started: " + args.StartedTask.Title, System.Diagnostics.EventLogEntryType.Information, 1006);
        }
    }
}
