using APITaskManagement.Logic.Schedulers.Repositories;
using Microsoft.AspNet.SignalR;
using System;

namespace APITaskManagement.Service.Hubs
{
    public class TaskSchedulerHub : Hub
    {
        public void UpdateTask(string id)
        {
            TaskRepository rep = new TaskRepository();
            var task = rep.GetById(new Guid(id));
            Clients.All.updateTask(id, task.Title, task.Active, task.LastRunTime.ToString("dd-MM-yyyy HH:mm:ss"), task.LastRunResult, task.Enabled);
        }

        public void UpdateTaskStatus(string id)
        {
            TaskRepository rep = new TaskRepository();
            var task = rep.GetById(new Guid(id));
            Clients.All.updateTaskStatus(id, task.Title, task.Enabled);
        }

        public void DisableTask(string id)
        {
            Clients.All.disableTask(id);
        }

        public void EnableTask(string id)
        {
            Clients.All.enableTask(id);
        }

        public void RunTask(string id)
        {
            Clients.All.runTask(id);
        }
    }
}
