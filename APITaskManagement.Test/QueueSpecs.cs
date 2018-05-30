using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using APITaskManagement.Logic.Schedulers;
using System.Collections.Generic;
using System.Linq;
using APITaskManagement.Logic.Queue;
using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Schedulers.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Queue.Repositories;

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
        public void QueueShouldSendRequests()
        {
            // var queue = new QueueTable("SalesOrders");
            var queue = new QueueZwaluwSalesorder("ZwaluwSalesOrders");
            var authentication = new Authentication("testclient", "testpass", AuthenticationType.None, null, "client_credentials", "http://api.profilan.local/oauth");
            queue.SendRequestsToTarget(HttpMethod.Post, new Url("test-zwaluw", "http://apitest.profilan.local/test", new Interval(30, Unit.Seconds)), authentication, new Guid("D0C86125 - 588E-46C0 - 8721 - D49612AF219F"));

            queue.GetNumberOfRequests().Should().Be(2);            
        }

        [TestMethod]
        public void CanGetQueuesFromRep()
        {
            var _rep = new QueueRepository();

            var items = _rep.List();

            items.Count().Should().Be(2);
        }

        [TestMethod]
        public void CanGetQueueByName()
        {
            var _rep = new QueueRepository();

            var queue = _rep.GetByName("SalesOrders");

            queue.Should().NotBeNull();
        }

        [TestMethod]
        public void GetJsonForHelloDialog()
        {
            var queue = new QueueHelloDialogEmail("HelloDialogEmails");
            var url = new Url("hello-dialog-email", "https://app.hellodialog.com/api/transactional/?token=49da55ec2bf98e892454914a65737194", new Interval(24, Unit.Hours));
            var authentication = new Authentication("", "", AuthenticationType.None);

            queue.SendRequestsToTarget(HttpMethod.Post, url, authentication, new Guid("8B0DF4EE-A851-46F5-9DEA-163136D024E5"));
        }
    }


}
