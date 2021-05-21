using ApiTaskManagement.Logic.Models;
using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Logging;
using APITaskManagement.Logic.ReceiveSend.ValueObjects;
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace APITaskManagement.Logic.Schedulers
{
    public class APITask : Task
    {
        protected APITask()
        {

        }

        public APITask(string title,
            int scheduleId,
            Schedule schedule,
            Authentication authentication,
            bool enabled,
            int totalProcesseditems = 100
            ) : base(title, scheduleId, schedule, authentication, enabled, totalProcesseditems)
        {

        }

        public override void Run()
        {
            Type t = Type.GetType("APITaskManagement.Logic.Api." + Classname);
            var api = (IApi)Activator.CreateInstance(t, Classname);
            api.TotalItems = TotalProcessedItems;
            api.AddLogger(new ApplicationLogger());
            api.AddLogger(new SystemLogger());
            if (HttpMethod == Common.HttpMethod.Get)
            {
                api.ReceiveResponseFromTarget(Url, Authentication, this);
            }
            else
            {
                api.SendRequestsToTarget(HttpMethod, Url, Authentication, this);
            }

            LatestResponse = api.GetLatestResponse();
        }
    }
}
