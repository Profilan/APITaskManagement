﻿using APITaskManagement.Logic.Api.Formatters;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api
{
    public class ApiSissyboyTrackAndTrace : Api
    {
        private readonly QueueRepository _queueRepository = new QueueRepository();
        protected override void ExecutePost(Request request)
        {
            throw new NotImplementedException();
        }

        public ApiSissyboyTrackAndTrace(string name) : base(name)
        {

        }

        public ApiSissyboyTrackAndTrace() : base()
        {

        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            var requests = new List<Request>();
            var items = _queueRepository.ListByTask(taskId, TotalItems);

            var formatter = new SissyboyTrackAndTraceFormatter();

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
