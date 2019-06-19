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
using APITaskScheduler.Logic;

[assembly: OwinStartup(typeof(APITaskManagement.Service.Startup))]
namespace APITaskManagement.Service
{
    public partial class APITaskScheduler : ServiceBase
    {
        private IScheduler _scheduler;
        public IHubProxy _hub { get; private set; }

        private string _url;

        public APITaskScheduler()
        {
            InitializeComponent();

            _url = SchedulerConfig.Settings.SignalR.HubConnectionUrl;
            _scheduler = new Scheduler();
            
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                WebApp.Start(_url);

                var connection = new HubConnection(_url);
                _hub = connection.CreateHubProxy("taskSchedulerHub");
                _hub.On<string>("disableTask", taskId => DisableTask(taskId));
                _hub.On<string>("enableTask", taskId => EnableTask(taskId));
                _hub.On<string>("runTask", taskId => RunTask(taskId));
                connection.Start().Wait();

                _scheduler.Start();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            
        }

        public void DisableTask(string taskId)
        {

            _scheduler.DisableTask(taskId);
            _hub.Invoke("UpdateTaskStatus", taskId);
        }

        public void EnableTask(string taskId)
        {
            _scheduler.EnableTask(taskId);
            _hub.Invoke("UpdateTaskStatus", taskId);
        }

        public void RunTask(string taskId) => _scheduler.Run(new Guid(taskId));

        protected override void OnStop()
        {
            _scheduler.Stop();
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
