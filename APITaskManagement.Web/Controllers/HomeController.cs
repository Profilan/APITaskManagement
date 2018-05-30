using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Schedulers.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APITaskManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Task, Guid> _taskRepository;

        public HomeController()
        {
            _taskRepository = new TaskRepository();
        }

        // GET: Task
        public ActionResult Index()
        {
            var tasks = _taskRepository.List();

            return View(tasks);
        }
        
    }
}