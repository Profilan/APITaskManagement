using APITaskManagement.Logic.Api.Formatters;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace APITaskManagement.Logic.Api
{
    public class ApiZwaluwInbound : Api
    {
        private readonly QueueRepository queueRepository = new QueueRepository();

        public ApiZwaluwInbound(string name) : base(name)
        {

        }

        public ApiZwaluwInbound() : base()
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

            var formatter = new ZwaluwInboundFormatter();

            foreach (var item in items)
            {
                var body = JsonConvert.DeserializeObject<ZwaluwIdentifierDto>(item.Body);
                var content = formatter.GetJsonContent(body.InboundShipmentHeaderId);

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
            return true;
        }
    }
}
