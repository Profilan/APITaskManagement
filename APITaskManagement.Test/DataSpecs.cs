﻿using System;
using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Api.Repositories;
using APITaskManagement.Logic.Filer.Repositories;
using APITaskManagement.Logic.Schedulers.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APITaskManagement.Test
{
    [TestClass]
    public class DataSpecs
    {
        private readonly PostNLRepository postNLRepository;
        

        public DataSpecs()
        {
            postNLRepository = new PostNLRepository();
        }

        [TestMethod]
        public void GetDeliveryDate()
        {
            var rep = new SissyboyRepository();

            var item = rep.GetDeliveryDateById(1);
        }

        [TestMethod]
        public void GetTrackAndTrace()
        {
            var rep = new SissyboyRepository();

            var item = rep.GetTrackAndTraceById(1);
        }

        [TestMethod]
        public void GetReadyForPickup()
        {
            var rep = new SissyboyRepository();

            var item = rep.GetReadyForPickupById(1);
        }

        [TestMethod]
        public void GetByIdPostNL()
        {
            var item = postNLRepository.GetById(5);

            item.Should().NotBeNull();
        }

        [TestMethod]
        public void GetTasks()
        {
            var rep = new TaskRepository();

            var items = rep.List();
        }
           

        [TestMethod]
        public void DutchNedSalesOrderList()
        {
            var rep = new DutchNedSalesOrderRepository();

            var items = rep.List();

            items.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void DutchNedSalesOrderGetById()
        {
            var rep = new DutchNedSalesOrderRepository();

            var item = rep.GetById(1);
        }

        [TestMethod]
        public void ZwaluwSalesOrderGetById()
        {
            var rep = new ZwaluwSalesOrderRepository();

            var item = rep.GetById(167214);
        }

        [TestMethod]
        public void InsertDutchNedInsertedOrderlinestatus()
        {
            DutchNedInsertedOrderlinestatusRepository dnRepo = new DutchNedInsertedOrderlinestatusRepository();

            var item = new DutchNedInsertedOrderlinestatus()
            {
                Status = 201,
                Description = "Dit is een test",
                OrderLineId = 999
            };

            dnRepo.Insert(item);
        }

        [TestMethod]
        public void InsertCashOnDelivery()
        {
            var rep = new CashOnDeliveryOrderlineRepository();

            var item = new CashOnDeliveryOrderline()
            {
                OrderlineId = 9997,
                CashOnDelivery = 550
            };

            rep.Insert(item);
        }

        [TestMethod]
        public void GetDHLDeliveries()
        {
            var rep = new DHLDeliveryRepository();

            var items = rep.List();
        }

        [TestMethod]
        public void GetTransMission()
        {
            var rep = new TransMissionRepository();

            var items = rep.List();
        }
    }
}
