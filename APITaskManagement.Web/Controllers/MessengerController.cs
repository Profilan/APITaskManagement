using APITaskManagement.Logic.Monitoring;
using APITaskManagement.Logic.Monitoring.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APITaskManagement.Web.Controllers
{
    public class MessengerController : Controller
    {
        protected readonly MessengerRepository _messengerRepository;

        public MessengerController()
        {
            _messengerRepository = new MessengerRepository();
        }

        // GET: Messenger
        public ActionResult Index()
        {
            var messengers = _messengerRepository.List();

            return View(messengers);
        }

        // GET: Messenger/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Messenger/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Messenger/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var messenger = new Messenger();
                messenger.Name = collection["Name"];
                var enabled = collection["Enabled"];
                if (enabled == "true,false")
                {
                    messenger.Enabled = true;
                }
                else
                {
                    messenger.Enabled = false;
                }

                _messengerRepository.Insert(messenger);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Messenger/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Messenger/Edit/5
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

        // GET: Messenger/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Messenger/Delete/5
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
