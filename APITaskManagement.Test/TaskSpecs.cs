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
using APITaskManagement.Logic.Queue;
using APITaskManagement.Logic.Management;

namespace APITaskManagement.Test
{
    [TestClass]
    public class TaskSpecs
    {
        private readonly IRepository<Task, Guid> _taskRepository;
        

        public TaskSpecs()
        {
            
            _taskRepository = new TaskRepository();

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
        public void TaskShouldSendRequests()
        {
            var task = _taskRepository.GetById(new Guid("09CD8B22-0FD3-41D1-9480-A8EF00E8226B"));

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
            var task = _taskRepository.GetById(new Guid("e2f93b02-97fd-4124-8286-a87200c08f40"));

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
    }
}
