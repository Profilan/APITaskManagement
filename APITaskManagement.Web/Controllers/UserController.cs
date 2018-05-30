using APITaskManagement.Logic.Common.Interfaces;
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
    public class UserController : Controller
    {
        private readonly IRepository<User, int> _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        // GET: User
        public ActionResult Index()
        {
            var users = _userRepository.List();

            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            var userViewModel = new UserViewModel()
            {
                
            };


            return View(userViewModel);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var user = new User(collection["Username"],
                    collection["Password"]);
                user.DisplayName = collection["DisplayName"];
                user.Apikey = collection["Apikey"];
                user.Email = collection["Email"];
                user.Enabled = Convert.ToBoolean(collection["Enabled"]);

                _userRepository.Insert(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _userRepository.GetById(id);

            var model = new UserViewModel()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Username = user.Username,
                Password = user.Password,
                Enabled = user.Enabled,
                Apikey = user.Apikey,
                Email = user.Email,
                
            };

            return View(model);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var user = _userRepository.GetById(id);

                user.Username = collection["Username"];
                user.Password = collection["Password"];
                user.DisplayName = collection["DisplayName"];
                user.Apikey = collection["Apikey"];
                user.Email = collection["Email"];
                user.Enabled = Convert.ToBoolean(collection["Enabled"]);
 
                _userRepository.Update(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
