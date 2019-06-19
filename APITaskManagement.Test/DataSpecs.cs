using System;
using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Api.Repositories;
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
        public void GetByIdPostNL()
        {
            var item = postNLRepository.GetById(5);

            item.Should().NotBeNull();
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
    }
}
