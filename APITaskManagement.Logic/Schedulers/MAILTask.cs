using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Logging;
using APITaskManagement.Logic.Mailer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Schedulers
{
    public class MAILTask : Task
    {
        public override void Send()
        {
            var formats = ContentFormats.Split(';');
            List<ContentFormat> contentFormats = new List<ContentFormat>();
            foreach (var format in formats)
            {
                contentFormats.Add((ContentFormat)Enum.Parse(typeof(ContentFormat), format));
            }
            var t = Type.GetType("APITaskManagement.Logic.Mailer." + Classname);
            var mailer = (IMailer)Activator.CreateInstance(t, contentFormats);
            mailer.AddLogger(new SystemLogger());
            mailer.Send(this);
            LatestResponse = mailer.GetLatestResponse();
        }
    }
}
