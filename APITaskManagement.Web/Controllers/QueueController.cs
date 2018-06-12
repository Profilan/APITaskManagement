using APITaskManagement.Logic.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APITaskManagement.Web.Controllers
{
    public class QueueController : Controller
    {
        private readonly QueueRepository queueRepository;

        public QueueController()
        {
            queueRepository = new QueueRepository();
        }

        // GET: Queue
        public ActionResult Index()
        {
            var items = queueRepository.List();

            return View(items);
        }

        // GET: Queue/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Queue/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Queue/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Queue/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Queue/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Queue/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Queue/Delete/5
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
