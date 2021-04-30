using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Api.Formatters;
using APITaskManagement.Logic.Api.Repositories;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api
{
    public class ApiFonQOfferFeedB2B : Api
    {
        private readonly QueueRepository _queueRepository;
        private readonly FonQOfferFeedB2BRepository _feedRepository = new FonQOfferFeedB2BRepository();

        public ApiFonQOfferFeedB2B(string name) : base(name)
        {
            _queueRepository = new QueueRepository();
        }

        public ApiFonQOfferFeedB2B() : base()
        {
            _queueRepository = new QueueRepository();
        }


        protected override void ExecutePost(Request request)
        {
            
        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            var requests = new List<Request>();
            var items = _queueRepository.ListByTask(taskId, TotalItems);

            var formatter = new FonQOfferFeedFormatterB2B();

            foreach (var item in items)
            {
                var feedItems = _feedRepository.List();

                foreach (var feedItem in feedItems)
                {
                    var content = formatter.GetJsonContent(feedItem);

                    if (!String.IsNullOrEmpty(content))
                    {
                        var request = new Request(item.Id, (int)item.Key, content, true);

                        requests.Add(request);
                    }
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
            return true;
        }
    }
}
