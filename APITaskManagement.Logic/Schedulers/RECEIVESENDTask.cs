using ApiTaskManagement.Logic.Models;
using APITaskManagement.Logic.Logging;
using APITaskManagement.Logic.ReceiveSend.Interfaces;
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace APITaskManagement.Logic.Schedulers
{
    public class RECEIVESENDTask : Task
    {
        public RECEIVESENDTask() { }

        public RECEIVESENDTask(string title,
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
            TextInfo info = new CultureInfo("en-US", false).TextInfo;
            string baseClassname = Regex.Replace(info.ToTitleCase(Title), @"\s+", "");

            string actionName = "APITaskManagement.Logic.ReceiveSend." + baseClassname;
            Type actionType = Type.GetType(actionName);
            if (actionType != null)
            {
                var action = Activator.CreateInstance(actionType, this) as ReceiveSendAction;
                action.AddLogger(new ApplicationLogger());
                action.AddLogger(new SystemLogger());
                action.Execute();
            }
            else
            {
                throw new FileNotFoundException("RECEIVESENDTask::Run()", actionName);
            }
        }

    }
}
