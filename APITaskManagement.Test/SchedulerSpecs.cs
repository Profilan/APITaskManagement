using System;
using System.Diagnostics;
using System.Timers;
using APITaskScheduler.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace APITaskManagement.Test
{
    [TestClass]
    public class SchedulerSpecs
    {
        

        public SchedulerSpecs()
        {
            
        }

        [TestMethod]
        public void SchedulerShouldStart()
        {
            var scheduler = new Scheduler();

            scheduler.Start();
        }

        [TestMethod]
        public void TaskShouldBeStarted()
        {
            

           
            

        }
    }
}
