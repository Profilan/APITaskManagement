using System.Collections.Generic;
using APITaskManagement.Logic.Schedulers;
using System.Configuration;
using System;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Api.Formatters;
using System.Net.Http;
using APITaskManagement.Logic.Management;

namespace APITaskManagement.Logic.Api
{
    public class ApiZwaluwSalesorder : Api
    {
        private readonly QueueRepository _queueRepository;

        public ApiZwaluwSalesorder(string name) : base(name)
        {
            _queueRepository = new QueueRepository();
        }

        public ApiZwaluwSalesorder() : base()
        {
            _queueRepository = new QueueRepository();
        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            var requests = new List<Request>();
            var items = _queueRepository.ListByTask(taskId, TotalItems);

            var formatter = new ZwaluwSalesOrderFormatter();

            foreach (var item in items)
            {
                var content = formatter.GetJsonContent(item.Key, Properties);

                var request = new Request(item.Id, (int)item.Key, content);

                requests.Add(request);
            }

            return requests;
        }

        protected override void ExecutePost(Request request)
        {
            throw new NotImplementedException();
        }

        protected override IList<ApiMessage> ProcessResponseForTask(string response)
        {
            throw new NotImplementedException();
        }

        protected override string RequestAcknowledgement()
        {
            throw new NotImplementedException();
        }

        protected override bool ExecuteBefore(HttpClient client, Request request, Url url)
        {
            throw new NotImplementedException();
        }
    }
}
