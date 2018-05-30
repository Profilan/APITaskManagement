using APITaskManagement.Logic.Monitoring;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static APITaskManagement.Logic.Monitoring.TaskScheduler;

namespace APITaskManagement.Logic.Management.Repositories
{
    public class SchedulerRepository
    {
        protected string _configFile;
        protected TaskScheduler _scheduler;

        public SchedulerRepository(string fileName)
        {
            _configFile = GetServiceConfigFileName(fileName);

            String xmlString = String.Empty;
            try
            {
                xmlString = System.IO.File.ReadAllText(fileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            _scheduler.TriggerItems.Clear();
            _scheduler = new TaskScheduler();
            var items = TaskScheduler.TriggerItemCollection.FromXML(xmlString);
            _scheduler.TriggerItems.AddRange(items, new TaskScheduler.TriggerItem.OnTriggerEventHandler(triggerItem_OnTrigger));
        }

        public TriggerItem GetById(string id)
        {
            foreach (TriggerItem item in _scheduler.TriggerItems)
            {
                if (item.Tag.ToString() == id)
                {
                    return item;
                }
            }

            return null;
        }

        public TaskScheduler.TriggerItemCollection List()
        {
            return _scheduler.TriggerItems;
        }

        private string GetServiceConfigFileName(string fileName)
        {
            String commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            String configDirectory = commonAppData + Path.DirectorySeparatorChar + "TaskScheduler";
            return configDirectory + Path.DirectorySeparatorChar + fileName;
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
