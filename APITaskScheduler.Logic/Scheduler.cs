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

        public event EventHandler<ElapsedEventArgs> Timer;

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

            try
            {
               
                var task = _taskRepository.GetById(taskId);

                DomainEvents.Register<TaskFinishedEvent>(OnTaskFinished);
                // DomainEvents.Register<TaskStartedEvent>(OnTaskStarted);

                if (task.Enabled && !task.Active)
                {
                    EventLog.WriteEntry("Starting task " + task.Title + "," + task.QueueName, System.Diagnostics.EventLogEntryType.Information, 1003);

#if DEBUG
                    System.Threading.Thread.Sleep(5000);
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
            finally
            {
                _timers[taskId.ToString()].Start();
            }
        }

        private void OnTaskStarted(TaskStartedEvent obj)
        {
            EventLog.WriteEntry("Task started: " + obj.StartedTask.Title, System.Diagnostics.EventLogEntryType.Information, 1006);

            obj.StartedTask.Active = true;
            _taskRepository.Update(obj.StartedTask);

            // _hub.Invoke("UpdateTaskStatus", obj.StartedTask.Id.ToString(), obj.StartedTask.Title, obj.StartedTask.IsActive(), obj.StartedTask.LastRunTime, obj.StartedTask.LastRunResult, obj.StartedTask.Enabled);
        }

        private void OnTaskFinished(TaskFinishedEvent obj)
        {
            EventLog.WriteEntry("Task finished: " + obj.FinishedTask.Title, System.Diagnostics.EventLogEntryType.Information, 1004);

            obj.FinishedTask.Active = false;
            _taskRepository.Update(obj.FinishedTask);

            //_hub.Invoke("UpdateTaskStatus", obj.FinishedTask.Id.ToString(), obj.FinishedTask.Title, obj.FinishedTask.IsActive(), obj.FinishedTask.LastRunTime, obj.FinishedTask.LastRunResult, obj.FinishedTask.Enabled);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

    }
}
