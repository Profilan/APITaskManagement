using APITaskManagement.Logic.Common.Data;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APITaskManagement.Web.Controllers.Api
{
    public class QueueController : ApiController
    {
        private readonly QueueRepository queueRepository;

        public QueueController()
        {
            queueRepository = new QueueRepository();
        }

        // GET: api/Queue
        public IHttpActionResult Get()
        {
            var items = queueRepository.List();

            List<QueueViewModel> queueItems = new List<QueueViewModel>();
            foreach (var item in items)
            {
                queueItems.Add(new QueueViewModel()
                {
                    Id = item.Id,
                    Key = item.Key,
                    Title = item.Task.Title,
                    SysCreated = item.SysCreated,
                    TryCount = item.TryCount
                });
            }

            return Ok(queueItems);
        }

        // GET: api/Queue/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Queue
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Queue/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Queue/5
        public void Delete(int id)
        {
        }
    }
}
