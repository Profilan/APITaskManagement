﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APITaskManagement.Logic.Filer.Repositories;
using FluentAssertions;
using APITaskManagement.Logic.Filer;
using APITaskManagement.Logic.Common;
using System.Collections.Generic;
using APITaskManagement.Logic.Filer.Interfaces;
using APITaskManagement.Logic.Mailer.Repositories;
using APITaskManagement.Logic.Mailer;

namespace APITaskManagement.Test
{
    [TestClass]
    public class DiskSpecs
    {
        private readonly ProductRepository productRepository;
        private readonly ImageRepository imageRepository;
        private readonly PackageRepository packageRepository;
        private readonly EazystockForecastRepository eazystockForecastRepository;

        public DiskSpecs()
        {
            productRepository = new ProductRepository();
            imageRepository = new ImageRepository();
            packageRepository = new PackageRepository();
            eazystockForecastRepository = new EazystockForecastRepository();
        }

        [TestMethod]
        public void ListEazystock()
        {
            var items = eazystockForecastRepository.List();

            items.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void GetProductList()
        {
            var products = productRepository.List();

            products.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void getProductById()
        {
            var product = productRepository.GetById("375481-Z");

            
        }

        [TestMethod]
        public void GetPosList()
        {
            var rep = new PosRepository();

            var orders = rep.List();

            orders.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void GetPostById()
        {
            var rep = new PosRepository();

            var order = rep.GetById(70005);

            order.Should().NotBeNull();
        }

        [TestMethod]
        public void getImagesByProductCode()
        {
            var images = imageRepository.ListByEANCode("8714713057887");

            
        }

        [TestMethod]
        public void getPackagesByProductCode()
        {
            var packages = packageRepository.ListByProductCode("360311-BET");
        }

        [TestMethod]
        public void SavePOS()
        {
            var formatter = new POSFormatter(ContentFormat.TXT);

            var result = formatter.getContent(70005);
        }
    }
}
