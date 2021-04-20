using APITaskManagement.Logic.Api.Formatters;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using MM.Monitor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api
{
    public class ApiDHLDeliveries : Api
    {
        private readonly QueueRepository queueRepository = new QueueRepository();

        public ApiDHLDeliveries(string name) : base(name)
        {
           
        }

        public ApiDHLDeliveries() : base()
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

            var formatter = new DHLDeliveriesFormatter();

            foreach (var item in items)
            {
                string content = formatter.GetXmlContent(item.Key, Properties);

                if (content != null)
                {
                    var request = new Request(item.Id, (int)item.Key, content, false, true);
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
            using (HttpClient client = new HttpClient())
            {
                var byteArraySha1 = new UTF8Encoding().GetBytes("DEE:" + GetSha1("RObKrqfIit"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArraySha1));

                var responseMessage = client.GetAsync("https://deliverit.dhl.com/webdsi/rest/latest/transmissionAcknowledgement/DEE").Result;

                var result = responseMessage.Content.ReadAsStringAsync().Result;

                return result;
            }
        }

        protected override bool ExecuteBefore(HttpClient client, Request request, Url url)
        {
            throw new NotImplementedException();
        }
    }
}
