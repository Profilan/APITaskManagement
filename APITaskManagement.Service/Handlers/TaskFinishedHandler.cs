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

namespace APITaskScheduler.Logic.Handlers
{
    public class TaskFinishedHandler : IHandle<TaskFinishedEvent>
    {
        private readonly TaskRepository _taskRepository = new TaskRepository();

        public void Handle(TaskFinishedEvent args)
        {
            
            args.FinishedTask.Active = false;
            _taskRepository.Update(args.FinishedTask);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TaskSchedulerHub>();
            hubContext.Clients.All.updateTask(args.FinishedTask.Id.ToString(), args.FinishedTask.Title, args.FinishedTask.Active, args.FinishedTask.LastRunTime.ToString("dd-MM-yyyy HH:mm:ss"), args.FinishedTask.LastRunResult, args.FinishedTask.Enabled);

            EventLog eventLog = new EventLog();
            eventLog.Source = "API Task Scheduler";
            eventLog.Log = "API Task Management";

            eventLog.WriteEntry("Task finished: " + args.FinishedTask.Title, System.Diagnostics.EventLogEntryType.Information, 1004);
        }
    }
}
