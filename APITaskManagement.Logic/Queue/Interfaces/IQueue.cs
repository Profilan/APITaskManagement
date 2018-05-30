using APITaskManagement.Logic.Logging.Interfaces;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Queue.Interfaces
{
    public interface IQueue
    {
        string Name { get; set; }

        IList<Request> Requests { get; }

        void SendRequestsToTarget(Common.HttpMethod httpMethod, Url url, Authentication authentication, Guid taskId);

        int GetNumberOfRequests();

        Response GetLatestResponse();

        void AddLogger(ILogger logger);
    }


}
