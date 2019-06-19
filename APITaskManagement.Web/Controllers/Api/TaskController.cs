using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Schedulers.Repositories;
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

        public TaskController()
        {
            _taskRepository = new TaskRepository();
        }

        // GET: api/Task
        public IEnumerable<Task> Get()
        {
            var tasks = _taskRepository.List();

            return tasks;
        }

        // GET: api/Task/{guid}
        public IHttpActionResult Get(string id)
        {
            var task = _taskRepository.GetById(new Guid(id));
            if (task == null)
            {
                return NotFound();
            }
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

        // DELETE: api/Task/5
        public void Delete(int id)
        {
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
