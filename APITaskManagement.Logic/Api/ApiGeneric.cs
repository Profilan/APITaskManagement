
using System.Collections.Generic;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Api.Interfaces;
using System;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Api.Formatters;
using System.Net.Http;
using APITaskManagement.Logic.Management;

namespace APITaskManagement.Logic.Api
{
    public class ApiGeneric : Api
    {
        private readonly QueueRepository queueRepository = new QueueRepository();

        public ApiGeneric(string name) : base(name)
        {

        }

        public ApiGeneric() : base()
        {

        }
        protected override void ExecutePost(Request request)
        {
            
        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            var requests = new List<Request>();
            var items = queueRepository.ListByTask(taskId, TotalItems);

            var formatter = new GenericFormatter();

            foreach (var item in items)
            {
                var content = formatter.GetJsonContent(item.Id, Properties);

                if (!String.IsNullOrEmpty(content))
                {
                    var request = new Request(item.Id, (int)item.Key, content, true);

                    requests.Add(request);
                }
            }

            return requests;
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
