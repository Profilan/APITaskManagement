
using System.Collections.Generic;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Api.Interfaces;
using System;

namespace APITaskManagement.Logic.Api
{
    public class ApiGeneric : Api
    {
        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            throw new System.NotImplementedException();
        }
    }
}
