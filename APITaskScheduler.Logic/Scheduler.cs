using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Schedulers.ApplicationEvents;
using APITaskManagement.Logic.Schedulers.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace APITaskScheduler.Logic
{
    public class Scheduler : IScheduler
    {
        public EventLog EventLog { get; private set; }
        public IDictionary<string, TaskTimer> _timers { get; private set; }

        private readonly TaskRepository _taskRepository = new TaskRepository();

        public Scheduler()
        {
            EventLog = new EventLog();

            _timers = new Dictionary<string, TaskTimer>();
           

            if (!System.Diagnostics.EventLog.SourceExists("API Task Scheduler"))
            {
                System.Diagnostics.EventLog.CreateEventSource("API Task Scheduler", "API Task Management");
            }
            EventLog.Source = "API Task Scheduler";
            EventLog.Log = "API Task Management";
        }

        public void Start()
        {
            try
            {
                EventLog.WriteEntry("API Task Scheduler started", System.Diagnostics.EventLogEntryType.Information, 0);

                EventLog.WriteEntry("Getting tasks", System.Diagnostics.EventLogEntryType.Information, 1);
                
                var tasks = _taskRepository.List();
                EventLog.WriteEntry("Number of tasks " + tasks.Count(), System.Diagnostics.EventLogEntryType.Information, 1);
                foreach (var task in tasks)
                {
                    TaskTimer timer = new TaskTimer(task.Id);
                    timer.Interval = 1000 * task.Interval.Seconds;
                    timer.Elapsed += OnTimer;

                    _timers.Add(task.Id.ToString(), timer);

                    timer.Start();

                    task.Active = false;
                    _taskRepository.Update(task);
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            Guid taskId = ((TaskTimer)sender).Id;

            Run(taskId);
        }

        public void Run(Guid taskId)
        {
            try
            {

                var task = _taskRepository.GetById(taskId);
                

                if (task.Enabled && !task.Active)
                {
                    EventLog.WriteEntry("Starting task " + task.Title + "," + task.QueueName, System.Diagnostics.EventLogEntryType.Information, 1003);
#if DEBUG
                    
                    task.Test(65000);
#else
                    task.Start();
#endif
                    EventLog.WriteEntry("Finishing task " + task.Title + "," + task.QueueName, System.Diagnostics.EventLogEntryType.Information, 1007);
                }

                
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("Error in executing task: " + e.Message, System.Diagnostics.EventLogEntryType.Error, 1010);
            }
        }

        public void Stop()
        {
            EventLog.WriteEntry("API Task Scheduler stopped", System.Diagnostics.EventLogEntryType.Information, 0);
        }

        public void DisableTask(string taskId)
        {
            _timers.TryGetValue(taskId, out TaskTimer timer);
            timer.Stop();

            var task = _taskRepository.GetById(new Guid(taskId));
            task.DisableTask();
            _taskRepository.Update(task);
        }

        public void EnableTask(string taskId)
        {
            _timers.TryGetValue(taskId, out TaskTimer timer);
            timer.Start();

            var task = _taskRepository.GetById(new Guid(taskId));
            task.EnableTask();
            _taskRepository.Update(task);
        }

    }
}
