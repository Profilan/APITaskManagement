using APITaskManagement.Logic.Api.Formatters;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Schedulers;
using MM.Monitor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    var request = new Request(item.Id, (int)item.Key, content);
                    requests.Add(request);
                }
            }

            return requests;
        }

        protected override IList<ApiMessage> ProcessResponseForTask(string response)
        {
            throw new NotImplementedException();
        }
    }
}
