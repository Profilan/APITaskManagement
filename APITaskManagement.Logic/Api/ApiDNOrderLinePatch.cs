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
    public class ApiDNOrderLinePatch : Api
    {
        private readonly QueueRepository queueRepository = new QueueRepository();

        public ApiDNOrderLinePatch(string name) : base(name)
        {

        }

        public ApiDNOrderLinePatch() : base()
        {

        }

        protected override void ExecutePost(Request request)
        {
            
        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
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
            return true;
        }
    }
}
