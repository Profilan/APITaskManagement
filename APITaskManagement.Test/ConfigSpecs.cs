using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Configuration;
using APITaskManagement.Logic.Config;
using APITaskManagement.Service.Config;

namespace APITaskManagement.Test
{
    [TestClass]
    public class ConfigSpecs
    {
        [TestMethod]
        public void CanReadFromConfig()
        {
            var _url = SchedulerConfig.Settings.SignalR.HubConnectionUrl;
        }
    }
}
