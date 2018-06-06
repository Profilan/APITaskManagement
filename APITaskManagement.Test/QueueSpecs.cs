using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using APITaskManagement.Logic.Schedulers;
using System.Collections.Generic;
using System.Linq;
using APITaskManagement.Logic.Api;
using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Schedulers.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Api.Repositories;

namespace APITaskManagement.Test
{
    [TestClass]
    public class QueueSpecs
    {
        [TestMethod]
        public void QueueShouldNotBeNull()
        {
            

            
        }

        [TestMethod]
        public void CanGetQueuesFromRep()
        {
            var _rep = new ApiRepository();

            var items = _rep.List();

            items.Count().Should().Be(2);
        }

        [TestMethod]
        public void CanGetQueueByName()
        {
            var _rep = new ApiRepository();

            var queue = _rep.GetByName("SalesOrders");

            queue.Should().NotBeNull();
        }
    }


}
