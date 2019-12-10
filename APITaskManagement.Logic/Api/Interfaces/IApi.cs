using APITaskManagement.Logic.Logging.Interfaces;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Api.Interfaces
{
    public interface IApi
    {
        string Name { get; set; }

        int TotalItems { get; set; }

        IList<Request> Requests { get; }

        void SendRequestsToTarget(Common.HttpMethod httpMethod, Url url, Authentication authentication, Task task);

        int GetNumberOfRequests();

        Response GetLatestResponse();

        void AddLogger(ILogger logger);
    }


}
