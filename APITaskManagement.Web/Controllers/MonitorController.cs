using APITaskManagement.Logic.Monitoring;
using APITaskManagement.Logic.Monitoring.Repositories;
using APITaskManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APITaskManagement.Web.Controllers
{
    public class MonitorController : Controller
    {
        protected MonitorRepository _monitorRepository;
        protected MessengerRepository _messengerRepository;

        public MonitorController()
        {
            _monitorRepository = new MonitorRepository();
            _messengerRepository = new MessengerRepository();
        }

        // GET: Monitor
        public ActionResult Index()
        {
            var items = _monitorRepository.List();

            return View(items);
        }

        // GET: Monitor/Details/5
        public ActionResult Details(Guid id)
        {

            return View();
        }

        // GET: Monitor/Create
        public ActionResult Create()
        {
            var messengers = _messengerRepository.List();

            var model = new MonitorViewModel()
            {
                Messengers = messengers
            };

            return View(model);
        }

        // POST: Monitor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var monitor = new Monitor();
                monitor.Name = collection["Name"];
                var enabled = collection["Enabled"];
                if (enabled == "true,false")
                {
                    monitor.Enabled = true;
                }
                else
                {
                    monitor.Enabled = false;
                }
                var messengers = collection["Messengers"].Split(',').ToList();
                foreach (var messenger in messengers)
                {
                    var item = _messengerRepository.GetById(new Guid(messenger));
                    
                    monitor.Messengers.Add(item);
                }

                _monitorRepository.Insert(monitor);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Monitor/Edit/5
        public ActionResult Edit(Guid id)
        {
            var monitor = _monitorRepository.GetById(id);
            var messengers = _messengerRepository.List();

            var viewModel = new MonitorViewModel()
            {
                Id = id,
                Name = monitor.Name,
                Enabled = monitor.Enabled,
                Messengers = messengers,
                SelectedMessengers = monitor.Messengers
            };

            return View(viewModel);
        }

        // POST: Monitor/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                var monitor = _monitorRepository.GetById(id);

                monitor.Name = collection["Name"];
                var enabled = collection["Enabled"];
                if (enabled == "true,false")
                {
                    monitor.Enabled = true;
                }
                else
                {
                    monitor.Enabled = false;
                }
                var messengers = collection["Messengers"].Split(',').ToList();
                foreach (var messenger in messengers)
                {
                    var item = _messengerRepository.GetById(new Guid(messenger));

                    monitor.Messengers.Add(item);
                }

                _monitorRepository.Update(monitor);
 
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Monitor/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: Monitor/Delete/5
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
