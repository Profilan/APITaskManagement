﻿using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api
{
    public class ApiDutchNedSalesOrder : Api
    {
        private readonly QueueRepository queueRepository = new QueueRepository();

        public ApiDutchNedSalesOrder(string name) : base(name)
        {
            queueRepository = new QueueRepository();
        }

        public ApiDutchNedSalesOrder() : base()
        {
            queueRepository = new QueueRepository();
        }

        protected override void ExecutePost(Request request)
        {
            throw new NotImplementedException();
        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            throw new NotImplementedException();
        }

        protected override IList<ApiMessage> ProcessResponseForTask(string response)
        {
            throw new NotImplementedException();
        }
    }
}
