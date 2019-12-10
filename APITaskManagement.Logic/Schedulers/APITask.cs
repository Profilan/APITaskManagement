using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers
{
    public class APITask : Task
    {
        protected APITask()
        {

        }

        public APITask(string title,
            int scheduleId,
            Interval interval,
            Authentication authentication,
            bool enabled,
            int totalProcesseditems = 100
            ) : base(title, scheduleId, interval, authentication, enabled, totalProcesseditems)
        {

        }

        public override void Send()
        {
            Type t = Type.GetType("APITaskManagement.Logic.Api." + Classname);
            var api = (IApi)Activator.CreateInstance(t, Classname);
            api.TotalItems = TotalProcessedItems;
            api.AddLogger(new ApplicationLogger());
            api.AddLogger(new SystemLogger());
            api.SendRequestsToTarget(HttpMethod, Url, Authentication, this);
            LatestResponse = api.GetLatestResponse();
        }
    }
}
