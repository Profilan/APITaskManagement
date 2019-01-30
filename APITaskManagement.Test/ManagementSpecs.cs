using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APITaskManagement.Logic.Management.Repositories;
using FluentAssertions;
using APITaskManagement.Logic.Common;
using System.Linq;
using APITaskManagement.Logic.Monitoring;
using static APITaskManagement.Logic.Monitoring.TaskScheduler;
using static APITaskManagement.Logic.Monitoring.TaskScheduler.TriggerItem;
using System.IO;
using APITaskManagement.Logic.Monitoring.Repositories;
using System.Collections.Generic;

namespace APITaskManagement.Test
{
    [TestClass]
    public class ManagementSpecs
    {
        protected TaskScheduler _taskScheduler;

        public ManagementSpecs()
        {
            _taskScheduler = new TaskScheduler();
            var triggerItem = new TriggerItem();
            triggerItem.Tag = "Test";
            triggerItem.StartDate = new DateTime(2018, 3, 28);
            triggerItem.EndDate = new DateTime(2018, 3, 30);
            triggerItem.TriggerTime = new DateTime(2018, 3, 29, 9, 30, 0);
            triggerItem.OnTrigger += new OnTriggerEventHandler(triggerItem_OnTrigger);
            triggerItem.Enabled = true;
            _taskScheduler.AddTrigger(triggerItem);
        }

        [TestMethod]
        public void GetUserList()
        {
            var rep = new UserRepository();

            var users = rep.List();

            users.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void GetList()
        {
            var rep = new LogRepository();

            var logs = rep.List();

            logs.Count().Should().Be(2);
        }

        [TestMethod]
        public void GetUnacknowledgedErrorsFromLog()
        {
            var rep = new LogRepository();
            DateTime start, end;

            var dateNow = DateTime.Now.AddMonths(-1);
            start = DateTime.Now.AddMonths(-1).Date;
            end = DateTime.Now.AddDays(1).Date;

            var logs = rep.List(start, end, ErrorType.ERR);

            logs.Count().Should().Be(2);
        }

        [TestMethod]
        public void SendErrorLog()
        {
            var rep = new LogRepository();
            DateTime start, end;

            var dateNow = DateTime.Now.AddMonths(-1);
            start = DateTime.Now.AddMonths(-1).Date;
            end = DateTime.Now.AddDays(1).Date;

            var logs = rep.List(start, end, ErrorType.ERR);

            var monitor = new EventsMonitor();
            var messengers = new HashSet<Messenger>();
            messengers.Add(new Mailer());

            monitor.Run(messengers);
        }

        [TestMethod]
        public void RunMonitor()
        {
            var rep = new MonitorRepository();

            var monitor = rep.GetById(new Guid("EB67F08C-BB86-4890-BF9D-A97B00D3B903"));
            var monitorToRun = rep.GetByName(monitor.Name);

            monitorToRun.Run(monitor.Messengers);
        }

        [TestMethod]
        public void GetMonitorList()
        {
            var rep = new MonitorRepository();

            var monitors = rep.List();

            monitors.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateMonitor()
        {
            var rep = new MonitorRepository();

            var monitor = new Monitor();
            monitor.Enabled = false;
            monitor.Name = "EventsMonitor";

            
            rep.Insert(monitor);
        }

        [TestMethod]
        public void SaveTaskSchedulerConfig()
        {

            String xmlString = ExportCollectionToXML();
            String configFile = GetServiceConfigFileName();

            String directory = Path.GetDirectoryName(configFile);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (StreamWriter outfile = new StreamWriter(configFile))
            {
                try
                {
                    outfile.Write(xmlString);
                    Console.WriteLine("Configuration saved successfully!" + Environment.NewLine + Environment.NewLine + "Filename: " + configFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: write XML: " + ex.ToString());
                }
            }
        }

        private string ExportCollectionToXML()
        {
            String xmlString = String.Empty;
            try
            {
                xmlString = _taskScheduler.TriggerItems.ToXML();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: generate XML: " + ex.ToString());
            }
            return xmlString;
        }

        private String GetServiceConfigFileName()
        {
            String commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            String configDirectory = commonAppData + Path.DirectorySeparatorChar + "TaskScheduler";
            return configDirectory + Path.DirectorySeparatorChar + "SchedulerItems.xml";
        }

        void triggerItem_OnTrigger(object sender, TaskScheduler.OnTriggerEventArgs e)
        {
            String nextTrigger = String.Empty;
            if (e.Item.GetNextTriggerDateTime() != DateTime.MaxValue)
                nextTrigger = e.Item.GetNextTriggerDateTime().DayOfWeek.ToString() + ", " + e.Item.GetNextTriggerDateTime().ToString();
            else
                nextTrigger = "Never";
            //textBoxEvents.AppendText(e.TriggerDate.ToString() + ": " + e.Item.Tag + ", next trigger: " + nextTrigger + "\r\n");
            //UpdateTaskList();
        }
    }
}
