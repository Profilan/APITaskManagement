using APITaskManagement.Logic.Api.Formatters;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace APITaskManagement.Logic.Api
{
    public class ApiZwaluwItem : Api
    {
        private readonly QueueRepository queueRepository = new QueueRepository();


        public ApiZwaluwItem(string name) : base(name)
        {

        }

        public ApiZwaluwItem() : base()
        {

        }
        protected override void ExecutePost(Request request)
        {
            throw new NotImplementedException();
        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            var requests = new List<Request>();
            var items = queueRepository.ListByTask(taskId, TotalItems);

            var formatter = new ZwaluwItemFormatter();

            foreach (var item in items)
            {
                var content = formatter.GetJsonContent(item.Key, Properties);

                var request = new Request(item.Id, (int)item.Key, content);

                requests.Add(request);
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
