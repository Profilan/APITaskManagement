using Microsoft.AspNet.SignalR;
using System;

namespace APITaskManagement.Service
{
    public class TaskSchedulerHub : Hub
    {
        public void UpdateTaskStatus(string id, string title, bool active, DateTime lastRunTime, string lastRunResult, bool enabled)
        {
            Clients.All.updateTaskStatus(id, title, active, lastRunTime.ToString("dd-MM-yyyy HH:mm:ss"), lastRunResult, enabled);
        }

        public void DisableTask(string id)
        {
            Clients.All.disableTask(id);
        }

        public void EnableTask(string id)
        {
            Clients.All.enableTask(id);
        }
    }
}
