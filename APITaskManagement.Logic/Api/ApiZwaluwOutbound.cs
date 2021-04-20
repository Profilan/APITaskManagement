using APITaskManagement.Logic.Api.Formatters;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace APITaskManagement.Logic.Api
{
    public class ApiZwaluwOutbound : Api
    {
        private readonly QueueRepository queueRepository = new QueueRepository();

        public ApiZwaluwOutbound(string name) : base(name)
        {

        }

        public ApiZwaluwOutbound() : base()
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

            var formatter = new ZwaluwOutboundFormatter();

            foreach (var item in items)
            {
                var body = JsonConvert.DeserializeObject<ZwaluwBodyDto>(item.Body);
                var content = formatter.GetJsonContent(item.Key, body.DeliveryDate);

                var request = new Request(item.Id, item.Key, content);
                if (body.ApiType == "PUT" || body.ApiType == "DELETE")
                {
                    request.ExecBefore = true;
                    request.Params.Add(("ediReference", body.EdiReference));
                }

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
            var responseMessage = client.DeleteAsync(url.Address + "?ediReference=" + request.Params[0].Value).Result;
            var statusCode = (int)responseMessage.StatusCode;

            if (statusCode < 400)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
