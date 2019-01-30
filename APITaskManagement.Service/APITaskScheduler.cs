using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Schedulers.ApplicationEvents;
using APITaskManagement.Logic.Schedulers.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Microsoft.AspNet.SignalR.Client;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Schedulers.Data;
using APITaskManagement.Service.Config;
using System.Configuration;
using APITaskManagement.Logic.Schedulers.Interfaces;

[assembly: OwinStartup(typeof(APITaskManagement.Service.Startup))]
namespace APITaskManagement.Service
{
    public partial class APITaskScheduler : ServiceBase
    {
        private readonly IRepository<Task, Guid> _taskRepository;
        private IDictionary<string, System.Timers.Timer> _timers { get; set; }
        public IHubProxy _hub { get; private set; }

        // Stack for checking if a task is already active
        public IDictionary<Guid, bool> RunningTasks { get; set; }

        private string _url;

        public APITaskScheduler()
        {
            InitializeComponent();

            RunningTasks = new Dictionary<Guid, bool>();

            eventLog1 = new System.Diagnostics.EventLog();
            _timers = new Dictionary<string, System.Timers.Timer>();
            _taskRepository = new TaskRepository();

            if (!System.Diagnostics.EventLog.SourceExists("API Task Scheduler"))
            {
                System.Diagnostics.EventLog.CreateEventSource("API Task Scheduler", "API Task Management");
            }
            eventLog1.Source = "API Task Scheduler";
            eventLog1.Log = "API Task Management";

            _url = SchedulerConfig.Settings.SignalR.HubConnectionUrl;
        }

        public void Start()
        {
            
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                eventLog1.WriteEntry("API Task Scheduler started", System.Diagnostics.EventLogEntryType.Information, 0);

                WebApp.Start(_url);

                var connection = new HubConnection(_url);
                _hub = connection.CreateHubProxy("taskSchedulerHub");
                _hub.On<string>("disableTask", taskId => DisableTask(taskId));
                _hub.On<string>("enableTask", taskId => EnableTask(taskId));
                connection.Start().Wait();

                eventLog1.WriteEntry("Getting tasks", System.Diagnostics.EventLogEntryType.Information, 1);
                var tasks = _taskRepository.List();
                eventLog1.WriteEntry("Number of tasks " + tasks.Count(), System.Diagnostics.EventLogEntryType.Information, 1);
                foreach (var task in tasks)
                {
                    System.Timers.Timer timer = new System.Timers.Timer();
                    timer.Interval = 1000 * task.Interval.Seconds;
                    timer.Elapsed += (sender, e) => this.OnTimer(sender, e, task.Id);

                    _timers.Add(task.Id.ToString(), timer);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void DisableTask(string taskId)
        {
            _timers.TryGetValue(taskId, out System.Timers.Timer timer);
            timer.Stop();
            
            var task = _taskRepository.GetById(new Guid(taskId));
            task.DisableTask();
            _taskRepository.Update(task);

            _hub.Invoke("UpdateTaskStatus", taskId, task.Title, task.IsActive(), task.LastRunTime, task.LastRunResult, task.Enabled);
        }

        public void EnableTask(string taskId)
        {
            _timers.TryGetValue(taskId, out System.Timers.Timer timer);
            timer.Start();
            
            var task = _taskRepository.GetById(new Guid(taskId));
            task.EnableTask();
            _taskRepository.Update(task);

            _hub.Invoke("UpdateTaskStatus", taskId, task.Title, task.IsActive(), task.LastRunTime, task.LastRunResult, task.Enabled);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("API Task Scheduler stopped", System.Diagnostics.EventLogEntryType.Information, 0);
            
        }

        protected void OnTimer(object sender, System.Timers.ElapsedEventArgs args, Guid id)
        {
            try
            {
                var task = _taskRepository.GetById(id);

                DomainEvents.Register<TaskFinishedEvent>(OnTaskFinished);
                // DomainEvents.Register<TaskStartedEvent>(OnTaskStarted);

                if (task.Enabled && !task.Active)
                {
                    eventLog1.WriteEntry("Starting task " + task.Title + "," + task.QueueName, System.Diagnostics.EventLogEntryType.Information, 1003);

#if DEBUG
                    System.Threading.Thread.Sleep(5000);
#else
                    task.Start();
#endif
                    eventLog1.WriteEntry("Finishing task " + task.Title + "," + task.QueueName, System.Diagnostics.EventLogEntryType.Information, 1007);
                }
            }
            catch (Exception e)
            {
                eventLog1.WriteEntry("Error in executing task: " + e.Message, System.Diagnostics.EventLogEntryType.Error, 1010);
            }
            finally
            {
                _timers[id.ToString()].Start();
            }
        }

        private void OnTaskStarted(TaskStartedEvent obj)
        {
            eventLog1.WriteEntry("Task started: " + obj.StartedTask.Title, System.Diagnostics.EventLogEntryType.Information, 1006);

            obj.StartedTask.Active = true;
            _taskRepository.Update(obj.StartedTask);

            _hub.Invoke("UpdateTaskStatus", obj.StartedTask.Id.ToString(), obj.StartedTask.Title, obj.StartedTask.IsActive(), obj.StartedTask.LastRunTime, obj.StartedTask.LastRunResult, obj.StartedTask.Enabled);
        }

        private void OnTaskFinished(TaskFinishedEvent obj)
        {
            eventLog1.WriteEntry("Task finished: " + obj.FinishedTask.Title, System.Diagnostics.EventLogEntryType.Information, 1004);

            obj.FinishedTask.Active = false;
            _taskRepository.Update(obj.FinishedTask);

            _hub.Invoke("UpdateTaskStatus", obj.FinishedTask.Id.ToString(), obj.FinishedTask.Title, obj.FinishedTask.IsActive(), obj.FinishedTask.LastRunTime, obj.FinishedTask.LastRunResult, obj.FinishedTask.Enabled);
        }
    }
}
