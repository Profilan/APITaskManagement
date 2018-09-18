﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Schedulers;

namespace APITaskManagement.Logic.Api
{
    public class ApiPostNLShipment : Api
    {
        private readonly QueueRepository _queueRepository;

        public ApiPostNLShipment(string name) : base(name)
        {
            _queueRepository = new QueueRepository();
        }

        public ApiPostNLShipment() : base()
        {
            _queueRepository = new QueueRepository();
        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            var requests = new List<Request>();
            var items = _queueRepository.ListByTask(taskId, 100);

            var formatter = new PostNLShipmentFormatter();

            foreach (var item in items)
            {
                var content = formatter.GetJsonContent(item.Key, Properties);

                var request = new Request(item.Id, (int)item.Key, content);

                requests.Add(request);
            }

            return requests;
        }
    }
}