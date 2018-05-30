using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Filer.Data;
using APITaskManagement.Logic.Filer.Repositories;
using APITaskManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APITaskManagement.Web.Controllers
{
    public class ShareController : Controller
    {
        private readonly IRepository<Share, int> _shareRepository;

        public ShareController()
        {
            _shareRepository = new ShareRepository();
        }

        // GET: Share
        public ActionResult Index()
        {
            var shares = _shareRepository.List();

            return View(shares);
        }

        // GET: Share/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Share/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Share/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var share = new Share();
                share.Name = collection["Name"];
                share.UNCPath = collection["UNCPath"];

                _shareRepository.Insert(share);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var message = e.Message;
                return View();
            }
        }

        // GET: Share/Edit/5
        public ActionResult Edit(int id)
        {
            var share = _shareRepository.GetById(id);

            var shareViewModel = new ShareViewModel()
            {
                Name = share.Name,
                UNCPath = share.UNCPath
            };

            return View(shareViewModel);
        }

        // POST: Share/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var share = _shareRepository.GetById(id);
                
                share.Name = collection["Name"];
                share.UNCPath = collection["UNCPath"];

                _shareRepository.Update(share);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Share/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Share/Delete/5
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
