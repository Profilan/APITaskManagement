using APITaskManagement.Logic.Common.Interfaces;
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
    public class ReportController : Controller
    {
        private readonly TaskRepository _taskRepository;
        private readonly LogRepository _logRepository;

        public ReportController()
        {
            _taskRepository = new TaskRepository();
            _logRepository = new LogRepository();
        }

        // GET: Report
        public ActionResult Index(string startDate = null, string endDate = null)
        {
            DateTime start, end;

            if (String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate))
            {
                var dateNow = DateTime.Now;
                start = dateNow.Date;
                end = start.AddDays(1);
            } 
            else
            {
                start = Convert.ToDateTime(startDate);
                end = Convert.ToDateTime(endDate);
            }

            var reportViewModel = new ReportViewModel { Logs = _logRepository.GetByDate(start, end) };

            ViewBag.StartDate = start.ToString("yyyy-MM-dd");
            ViewBag.EndDate = end.ToString("yyyy-MM-dd");

            return View(reportViewModel);
        }
    }
}