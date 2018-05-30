using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Filer.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Queue;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Schedulers.Data;
using APITaskManagement.Logic.Schedulers.Repositories;
using APITaskManagement.Logic.Schedulers.Services;
using APITaskManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APITaskManagement.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly IRepository<Task, Guid> _taskRepository;
        private readonly IQueueRepository _queueRepository;
        private readonly IRepository<Target, int> _targetRepository;
        private readonly IRepository<Url, int> _urlRepository;
        private readonly IRepository<Share, int> _shareRepository;

        public TaskController()
        {
            _taskRepository = new TaskRepository();
            _queueRepository = new QueueRepository();
            _targetRepository = new TargetRepository();
            _urlRepository = new UrlRepository();
            _shareRepository = new ShareRepository();
        }

        // GET: Task
        public ActionResult Index()
        {
            var tasks = _taskRepository.List();

            return View(tasks);
        }

        // GET: Task/Details/{guid}
        public ActionResult Details(Guid id)
        {
            var task = _taskRepository.GetById(id);

            return View(task);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            var queues = _queueRepository.List();
            var urls = _urlRepository.List();
            var shares = _shareRepository.List();

            var diskFormats = new[]
            {
                new SelectListItem { Value = "1", Text = "JSON" },
                new SelectListItem { Value = "2", Text = "XML" },
            };

            var taskViewModel = new TaskViewModel
            {
                Queues = queues,
                Urls = urls,
                DiskFormats = diskFormats,
                Shares = shares
            };

            return View(taskViewModel);
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Interval interval = new Interval(
                    Convert.ToInt32(collection["Amount"]),
                    (Unit)Enum.Parse(typeof(Unit), collection["Unit"]));

                Authentication authentication = new Authentication(collection["Username"],
                    collection["Password"],
                    (AuthenticationType)Enum.Parse(typeof(AuthenticationType), collection["AuthenticationType"]),
                    collection["Scope"],
                    collection["GrantType"],
                    collection["OAuthUrl"]);

                var target = _targetRepository.GetById(Convert.ToInt32(collection["TargetId"]));
                var url = _urlRepository.GetById(Convert.ToInt32(collection["UrlId"]));

                var task = new Logic.Schedulers.Task(collection["Title"],
                    1,
                    interval,
                    authentication,
                    false);

                task.MaxErrors = Convert.ToInt32(collection["MaxErrors"]);
                task.TaskType = (TaskType)Enum.Parse(typeof(TaskType), collection["TaskType"]);
                
                // task.Disk.UNCPath = collection["DiskUNCPath"];
                task.Classname = collection["DiskClassname"];
                task.ContentFormats = String.Join(";", collection["SelectedDiskFormats"]);
                task.QueueName = collection["QueueName"];
                task.Url = url;

                var selectedShares = collection["SelectedShares"].Split(',');
                foreach (var shareId in selectedShares)
                {
                    var share = _shareRepository.GetById(Convert.ToInt32(shareId));
                    task.Shares.Add(share);
                }

                _taskRepository.Insert(task);

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: Task/Edit/{guid}
        public ActionResult Edit(Guid id)
        {
            var task = _taskRepository.GetById(id);
            var queues = _queueRepository.List();
            var targets = _targetRepository.List();
            var urls = _urlRepository.List();
            var shares = _shareRepository.List();

            var diskFormats = new[]
            {
                new SelectListItem { Value = "1", Text = "JSON" },
                new SelectListItem { Value = "2", Text = "XML" },
            };

            List<int> selectedDiskFormats = new List<int>();
            try
            {
                var selectedFormats = task.ContentFormats.Split(';');

                foreach (var selectedFormat in selectedFormats)
                {
                    selectedDiskFormats.Add(Convert.ToInt32(selectedFormat));
                }
            }
            catch (Exception)
            {
                
            }
            string uncPath;
            try
            {
                // uncPath = task.Disk.UNCPath;
            }
            catch (Exception)
            {
                uncPath = "";
            }
            string classname;
            try
            {
                classname = task.Classname;
            }
            catch (Exception)
            {
                classname = "";
            }

            var taskViewModel = new TaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                QueueName = task.QueueName,
                Url = task.Url.Address,
                HttpMethod = task.HttpMethod,
                Username = task.Authentication.Username,
                Password = task.Authentication.Password,
                OAuthUrl = task.Authentication.OAuthUrl,
                GrantType = task.Authentication.GrantType,
                AuthenticationType = task.Authentication.AuthenticationType,
                TaskType = task.TaskType,
                Scope = task.Authentication.Scope,
                Amount = task.Interval.Amount,
                Unit = task.Interval.Unit,
                Queues = queues,
                Urls = urls,
                UrlId = task.Url.Id,
                MaxErrors = task.MaxErrors,
                // DiskUNCPath = uncPath,
                DiskClassname = classname,
                DiskFormats = diskFormats,
                SelectedDiskFormats = selectedDiskFormats.ToArray(),
                Shares = shares,
                SelectedShares = task.Shares
            };

            return View(taskViewModel);
        }

        // POST: Task/Edit/{guid}
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                var task = _taskRepository.GetById(id);
                var url = _urlRepository.GetById(Convert.ToInt32(collection["UrlId"]));

                task.QueueName = collection["QueueName"];
                task.Title = collection["Title"];

                task.HttpMethod = (HttpMethod)Enum.Parse(typeof(HttpMethod), collection["HttpMethod"]);
                task.Url = url;

                Interval interval = new Interval(
                     Convert.ToInt32(collection["Amount"]),
                     (Unit)Enum.Parse(typeof(Unit), collection["Unit"]));

                task.Interval = interval;

                Authentication authentication = new Authentication(collection["Username"],
                    collection["Password"],
                    (AuthenticationType)Enum.Parse(typeof(AuthenticationType), collection["AuthenticationType"]),
                    collection["Scope"],
                    collection["GrantType"],
                    collection["OAuthUrl"]);

                task.Authentication = authentication;
                task.MaxErrors = Convert.ToInt32(collection["MaxErrors"]);
                task.TaskType = (TaskType)Enum.Parse(typeof(TaskType), collection["TaskType"]);
                // task.Disk.UNCPath = collection["DiskUNCPath"];
                task.Classname = collection["DiskClassname"];
                task.ContentFormats = String.Join(";", collection["SelectedDiskFormats"]);

                task.Shares.Clear();
                var selectedShares = collection["SelectedShares"].Split(',');
                foreach (var shareId in selectedShares)
                {
                    var share = _shareRepository.GetById(Convert.ToInt32(shareId));
                    task.Shares.Add(share);
                }

                _taskRepository.Update(task);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: Task/Delete/{guid}
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: Task/Delete/{guid}
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
