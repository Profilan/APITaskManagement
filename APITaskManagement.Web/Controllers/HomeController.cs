using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Schedulers.Repositories;
using APITaskManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APITaskManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Task, Guid> _taskRepository = new TaskRepository();
        private readonly QueueRepository _queueRepository = new QueueRepository();

        // GET: Task
        public ActionResult Index()
        {
            var items = _taskRepository.List();

            var tasks = new List<TaskViewModel>();
            foreach (var task in items)
            {
                var queued = _queueRepository.List().Where(x => x.Task.Id == task.Id).Count();

                tasks.Add(new TaskViewModel()
                {
                    Id = task.Id,
                    Title = task.Title,
                    Enabled = task.Enabled,
                    LastRunTime = task.LastRunTime,
                    LastRunResult = task.LastRunResult,
                    Active = task.Active,
                    Queued = queued
                });
            }

            return View(tasks);
        }
        
    }
}