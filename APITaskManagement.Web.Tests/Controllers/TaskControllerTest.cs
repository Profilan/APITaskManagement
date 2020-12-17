using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APITaskManagement.Web;
using APITaskManagement.Web.Controllers;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers.Repositories;
using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Filer.Repositories;
using APITaskManagement.Logic.Filer.Data;

namespace APITaskManagement.Web.Tests.Controllers
{
    [TestClass]
    public class TaskControllerTest
    {
        private readonly IRepository<Task, Guid> _taskRepository;
        private readonly IRepository<Url, int> _urlRepository;
        private readonly IRepository<Share, int> _shareRepository;

        public TaskControllerTest()
        {
            _taskRepository = new TaskRepository();
            _urlRepository = new UrlRepository();
            _shareRepository = new ShareRepository();
        }


        [TestMethod]
        public void Index()
        {
            // Arrange
            TaskController controller = new TaskController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

   
 
    }
}
