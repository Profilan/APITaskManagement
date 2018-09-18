using System;
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
    }
}
