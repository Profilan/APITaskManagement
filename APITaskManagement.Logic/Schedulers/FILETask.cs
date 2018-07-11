using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Filer.Interfaces;
using APITaskManagement.Logic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers
{
    public class FILETask : Task
    {
        protected FILETask()
        {

        }

        public FILETask(string title,
            int scheduleId,
            Interval interval,
            Authentication authentication,
            bool enabled
            ) : base(title, scheduleId, interval, authentication, enabled)
        {

        }
        public override void Send()
        {
            var formats = ContentFormats.Split(';');
            List<ContentFormat> contentFormats = new List<ContentFormat>();
            foreach (var format in formats)
            {
                contentFormats.Add((ContentFormat)Enum.Parse(typeof(ContentFormat), format));
            }
            var t = Type.GetType("APITaskManagement.Logic.Filer." + Classname);
            var filer = (IFiler)Activator.CreateInstance(t, contentFormats);
            filer.AddLogger(new SystemLogger());
            filer.Send(Shares, this.Id);
            LatestResponse = filer.GetLatestResponse();
        }
    }
}
