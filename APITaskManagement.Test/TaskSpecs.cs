using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Common;
using FluentAssertions;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Schedulers.Repositories;
using System.Collections.Generic;
using APITaskManagement.Logic.Schedulers.ApplicationEvents;
using System.Linq;
using APITaskManagement.Logic.Api;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers.Interfaces;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using StructureMap;
using APITaskManagement.Test.DependencyResolution;

namespace APITaskManagement.Test
{
    [TestClass]
    public class TaskSpecs
    {
        private readonly IRepository<Task, Guid> _taskRepository;
        

        public TaskSpecs()
        {
            
            _taskRepository = new TaskRepository();

            // NHibernateProfiler.Initialize();

            IContainer container = IoC.Initialize();
            DomainEvents.Container = container;

        }



        public void Run_tasks()
        {
            var tasks = _taskRepository.List();
            foreach (var task in tasks)
            {
                if (task.Enabled)
                {
                    task.Start();

                }
            }
        }

       

        [TestMethod]
        public void TaskShouldRun()
        {
            var task = _taskRepository.GetById(new Guid("ae03f9c6-5a66-497d-885d-aca7009e05c5"));

            task.Start();

        }

        [TestMethod]
        public void LastRunCanBeChanged()
        {
            var tasks = _taskRepository.List();
            var task = tasks.First();

            var date = DateTime.Now;
            var dateString = date.ToString("dd-MM-yyyy HH:mm:ss");
            task.ChangeLastRun("201 Created", date, "Test");

            _taskRepository.Update(task);

            var updatedTask = _taskRepository.GetById(task.Id);

            updatedTask.LastRunTime.ToString("dd-MM-yyyy HH:mm:ss").Should().Be(dateString);



            
        }

        [TestMethod]
        public void GetTaskById()
        {
            // DutchNed e2f93b02-97fd-4124-8286-a87200c08f40
            // Zwaluw d0c86125-588e-46c0-8721-d49612af219f
            var task = _taskRepository.GetById(new Guid("ae03f9c6-5a66-497d-885d-aca7009e05c5"));

            task.Start();
        }

        private void OnTaskStarted(TaskStartedEvent obj)
        {
            Console.WriteLine("Task started");
        }

        private void OnTaskFinished(TaskFinishedEvent obj)
        {
            Console.WriteLine("Task stopped");
        }

        [TestMethod]
        public void TestResponseCreation()
        {
            var api = new ApiDNSalesOrder("DutchNed test");

            var request = new Request(999, 999, "{\"Lorem\":\"Ipsum\"", true);
            var response = new Response(200, "200 OK", "{\"status\":\"success\",\"results\":[{\"id\":\"172430\",\"status\":603},{\"id\":\"175188\",\"status\":602}]}");
            request.SetResponse(response);

            api.TestExecutePost(request);
        }


    }
}
