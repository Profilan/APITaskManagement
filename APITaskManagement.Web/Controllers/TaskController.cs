﻿using ApiTaskManagement.Logic.Models;
using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Repositories;
using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Filer.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Management.Repositories;
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
    public class TaskController : Controller
    {
        private readonly IRepository<Task, Guid> _taskRepository;
        private readonly IRepository<Url, int> _urlRepository;
        private readonly IRepository<Share, int> _shareRepository;

        public TaskController()
        {
            _taskRepository = new TaskRepository();
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
            var urls = _urlRepository.List();
            var shares = _shareRepository.List();

            var formats = new[]
            {
                new SelectListItem { Value = "1", Text = "JSON" },
                new SelectListItem { Value = "2", Text = "XML" },
                new SelectListItem { Value = "3", Text = "POS" },
            };

            var taskViewModel = new TaskViewModel
            {
                Urls = urls,
                Formats = formats,
                Shares = shares,
                SelectedShares = new HashSet<Share>()
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

                Schedule schedule = new Schedule((ScheduleType)Enum.Parse(typeof(ScheduleType), collection["ScheduleType"]),
                    DateTime.Parse(collection["ScheduleStart"]),
                    DateTime.Parse(collection["ScheduleEnd"]),
                    collection["ScheduleDays"],
                    Convert.ToInt32(collection["ScheduleRecurrence"]),
                    interval
                    );

                Authentication authentication = new Authentication(collection["Username"],
                    collection["Password"],
                    (AuthenticationType)Enum.Parse(typeof(AuthenticationType), collection["AuthenticationType"]),
                    collection["Scope"],
                    collection["GrantType"],
                    collection["OAuthUrl"]);

                var url = _urlRepository.GetById(Convert.ToInt32(collection["UrlId"]));

                var taskType = (TaskType)Enum.Parse(typeof(TaskType), collection["TaskType"]);
                Task task = new Task();
                switch (taskType)
                {
                    case TaskType.API:

                        task = new Logic.Schedulers.APITask(collection["Title"],
                                            1,
                                            schedule,
                                            authentication,
                                            false);
                        break;
                    case TaskType.FILE:
                        task = new Logic.Schedulers.FILETask(collection["Title"],
                                            1,
                                            schedule,
                                            authentication,
                                            false);
                        break;
                    case TaskType.MAIL:
                        task = new Logic.Schedulers.MAILTask(collection["Title"],
                                            1,
                                            schedule,
                                            authentication,
                                            false);
                        break;

                }

                task.MaxErrors = Convert.ToInt32(collection["MaxErrors"]);
                task.TotalProcessedItems = Convert.ToInt32(collection["TotalProcessedItems"]);
                task.TaskType = (TaskType)Enum.Parse(typeof(TaskType), collection["TaskType"]);
                
                task.Classname = collection["Classname"];
                task.ContentFormats = String.Join(";", collection["SelectedfFormats"]);
                task.Url = url;

                try
                {
                    var selectedShares = collection["SelectedShares"].Split(',');
                    foreach (var shareId in selectedShares)
                    {
                        var share = _shareRepository.GetById(Convert.ToInt32(shareId));
                        task.Shares.Add(share);
                    }
                }
                catch (Exception)
                {

                }

                task.SPLogger = collection["SPLogger"];
 
                task.MailSender = collection["MailSender"];
                task.MailRecipient = collection["MailRecipient"];

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
            var urls = _urlRepository.List();
            var shares = _shareRepository.List();

            var formats = new[]
            {
                new SelectListItem { Value = "1", Text = "JSON" },
                new SelectListItem { Value = "2", Text = "XML" },
                new SelectListItem { Value = "3", Text = "TXT" }
            };

            List<int> selectedfFormats = new List<int>();
            try
            {
                var selectedFormats = task.ContentFormats.Split(';');

                foreach (var selectedFormat in selectedFormats)
                {
                    selectedfFormats.Add(Convert.ToInt32(selectedFormat));
                }
            }
            catch (Exception)
            {
                
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
            string spLogger;
            try
            {
                spLogger = task.SPLogger;
            }
            catch (Exception)
            {
                spLogger = "";
            }

            string apiKey;
            try
            {
                apiKey = task.Authentication.ApiKey;
            }
            catch (Exception)
            {
                apiKey = "";
            }

            var taskViewModel = new TaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                HttpMethod = task.HttpMethod,
                Username = task.Authentication.Username,
                Password = task.Authentication.Password,
                OAuthUrl = task.Authentication.OAuthUrl,
                GrantType = task.Authentication.GrantType,
                AuthenticationType = task.Authentication.AuthenticationType,
                TaskType = task.TaskType,
                Scope = task.Authentication.Scope,
                Amount = task.Schedule.Interval.Amount,
                Unit = task.Schedule.Interval.Unit,
                Urls = urls,
                MaxErrors = task.MaxErrors,
                Classname = classname,
                Formats = formats,
                SelectedFormats = selectedfFormats.ToArray(),
                Shares = shares,
                SelectedShares = task.Shares,
                SPLogger = task.SPLogger,
                ApiKey = apiKey,
                TotalProcessedItems = task.TotalProcessedItems,
                Headers = task.HttpHeaders
            };
            try
            {
                taskViewModel.UrlId = task.Url.Id;
            }
            catch (Exception)
            {

            }
            try
            {
                taskViewModel.Url = task.Url.Address;
            }
            catch (Exception)
            {

            }
            try
            {
                taskViewModel.MailSender = task.MailSender;
            }
            catch (Exception)
            {
                taskViewModel.MailRecipient = "";
            }
            try
            {
                taskViewModel.MailRecipient = task.MailRecipient;
            }
            catch (Exception)
            {
                taskViewModel.MailRecipient = "";
            }

            return View(taskViewModel);
        }

        // POST: Task/Edit/{guid}
        [HttpPost]
        public ActionResult Edit(Guid id, TaskViewModel data)
        {
            try
            {
                var task = _taskRepository.GetById(id);
                var url = _urlRepository.GetById(data.UrlId);

                task.Title = data.Title;

                task.HttpMethod = data.HttpMethod;
                task.Url = url;

                Interval interval = new Interval(
                     data.Amount, data.Unit);

                task.Schedule = new Schedule(data.ScheduleType,
                    data.ScheduleStart,
                    data.ScheduleEnd,
                    data.ScheduleDays,
                    data.ScheduleRecurrence,
                    interval
                    );

                Authentication authentication = new Authentication(data.Username,
                    data.Password,
                    data.AuthenticationType,
                    data.Scope,
                    data.GrantType,
                    data.OAuthUrl);
                authentication.ApiKey = data.ApiKey;

                task.Authentication = authentication;
                task.MaxErrors = data.MaxErrors;
                task.TotalProcessedItems = data.TotalProcessedItems;
                task.TaskType = data.TaskType;
                
                // task.Disk.UNCPath = collection["DiskUNCPath"];
                task.Classname = data.Classname;
                task.ContentFormats = String.Join(";", data.SelectedFormats);

                string spLogger;
                try
                {
                    spLogger = task.SPLogger;
                }
                catch (Exception)
                {
                    spLogger = "";
                }

                try
                {
                    task.Shares.Clear();
                    var selectedShares = data.SelectedShares;
                    
                }
                catch
                {

                }

                task.MailSender = data.MailSender;
                task.MailRecipient = data.MailRecipient;

                try
                {
                    // task.HttpHeaders.Clear();
                    foreach (var header in data.Headers)
                    {
                        task.AddHeader(new HttpHeader(header.Name, header.Value));
                    }
                    
                }
                catch
                {

                }

                switch (data.TaskType)
                {
                    case TaskType.API:
                        _taskRepository.Update((APITask)task);
                        break;
                    case TaskType.FILE:
                        _taskRepository.Update((FILETask)task);
                        break;
                    case TaskType.MAIL:
                        _taskRepository.Update((MAILTask)task);
                        break;

                }
                

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
