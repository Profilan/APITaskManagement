using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Schedulers.Repositories;
using System;
using System.Web.Mvc;

namespace APITaskManagement.Web.Controllers
{
    public class LogController : Controller
    {
        private readonly LogRepository _logRepository;

        public LogController()
        {
            _logRepository = new LogRepository();
        }

        // GET: Log
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string startDate = null, string endDate = null)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TimestampSortParm = String.IsNullOrEmpty(sortOrder) ? "timestamp_desc" : "";
            // ViewBag.PrioritySortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            DateTime start, end;

            if (String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate))
            {
                var dateNow = DateTime.Now.AddMonths(-1);
                start = DateTime.Now.AddMonths(-1).Date;
                end = DateTime.Now.AddDays(1).Date;
            }
            else
            {
                start = Convert.ToDateTime(startDate);
                end = Convert.ToDateTime(endDate);
            }

            ViewBag.StartDate = start.ToString("yyyy-MM-dd");
            ViewBag.EndDate = end.ToString("yyyy-MM-dd");

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var logs = _logRepository.List(sortOrder, searchString, pageSize, pageNumber, start, end);

            return View(logs);
        }

        // GET: Log/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
