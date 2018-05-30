using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APITaskManagement.Web.Controllers
{
    public class UrlController : Controller
    {
        private readonly IRepository<Url, int> _urlRepository;

        public UrlController()
        {
            _urlRepository = new UrlRepository();
        }

        // GET: Url
        public ActionResult Index()
        {
            var urls = _urlRepository.List();

            return View(urls);
        }

        // GET: Url/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Url/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Url/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var inactivityTimeout = new Interval(Convert.ToInt32(collection["Amount"]),
                    (Unit)Enum.Parse(typeof(Unit), collection["Unit"]));

                var url = new Url(collection["Name"],
                    collection["Address"],
                    inactivityTimeout);
                url.ExternalUrl = collection["ExternalUrl"];

                _urlRepository.Insert(url);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Url/Edit/5
        public ActionResult Edit(int id)
        {
            var url = _urlRepository.GetById(id);

            var urlViewModel = new UrlViewModel()
            {
                Name = url.Name,
                Address = url.Address,
                Amount = url.InactivityTimeout.Amount,
                Unit = url.InactivityTimeout.Unit,
                ExternalUrl = url.ExternalUrl
            };

            return View(urlViewModel);
        }

        // POST: Url/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var inactivityTimeout = new Interval(Convert.ToInt32(collection["Amount"]),
                   (Unit)Enum.Parse(typeof(Unit), collection["Unit"]));

                var url = _urlRepository.GetById(id);
                url.Address = collection["Address"];
                url.Name = collection["Name"];
                url.InactivityTimeout = inactivityTimeout;
                url.ExternalUrl = collection["ExternalUrl"];

                _urlRepository.Update(url);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Url/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Url/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
