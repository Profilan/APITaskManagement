using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Schedulers.Repositories;
using APITaskManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APITaskManagement.Web.Controllers.Api
{
    public class TaskController : ApiController
    {
        private readonly IRepository<Task, Guid> _taskRepository;
        private readonly QueueRepository _queueRepository = new QueueRepository();

        public TaskController()
        {
            _taskRepository = new TaskRepository();
        }

        [HttpGet]
        [Route("api/task")]
        public IHttpActionResult Get()
        {
            var items = _taskRepository.List();

            var tasks = new List<TaskApiModel>();
            foreach (var task in items)
            {
                var queued = _queueRepository.List().Where(x => x.Task.Id == task.Id).Count();

                tasks.Add(new TaskApiModel()
                {
                    Id = task.Id,
                    Title = task.Title,
                    Enabled = task.Enabled,
                    LastRunTime = task.LastRunTime,
                    LastRunResult = task.LastRunResult,
                    LastRunDetails = task.LastRunDetails,
                    Active = task.Active,
                    Queued = queued
                });
            }

            return Ok(tasks);
        }

        [HttpGet]
        [Route("api/task/{id}")]
        public IHttpActionResult Get(string id)
        {
            var item = _taskRepository.GetById(new Guid(id));
            if (item == null)
            {
                return NotFound();
            }

            var queued = _queueRepository.List().Where(x => x.Task.Id == item.Id).Count();

            var task = new TaskApiModel()
            {
                Id = item.Id,
                Title = item.Title,
                Enabled = item.Enabled,
                LastRunTime = item.LastRunTime,
                LastRunResult = item.LastRunResult,
                LastRunDetails = item.LastRunDetails,
                Active = item.Active,
                Queued = queued
            };

            return Ok(task);
        }

        // POST: api/Task
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Task/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // POST: Url/Delete/5
        [Route("api/Queue/Delete/{id}")]
        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _queueRepository.Delete(id);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        [HttpPost]
        [Route("api/task/reset/{id}")]
        public IHttpActionResult Reset(Guid id)
        {
            var task = _taskRepository.GetById(id);
            if (task != null)
            {
                task.Active = false;
                _taskRepository.Update(task);
            }
            else
            {
                return NotFound();
            }
 
            return Ok(new { message = task.Title + " succesfully reset" });
        }
    }
}
