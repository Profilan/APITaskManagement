﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APITaskManagement.Web;
using APITaskManagement.Web.Controllers;

namespace APITaskManagement.Web.Tests.Controllers
{
    [TestClass]
    public class TaskControllerTest
    {
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
