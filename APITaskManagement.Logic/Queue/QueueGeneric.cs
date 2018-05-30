
using System.Collections.Generic;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Queue.Interfaces;
using System;

namespace APITaskManagement.Logic.Queue
{
    public class QueueGeneric : Queue
    {
        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            throw new System.NotImplementedException();
        }
    }
}
