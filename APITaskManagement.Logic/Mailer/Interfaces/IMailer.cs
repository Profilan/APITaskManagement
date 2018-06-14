using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Logging.Interfaces;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APITaskManagement.Logic.Mailer.Interfaces
{
    public interface IMailer
    {
        IList<ContentFormat> Formats { get; set; }
        IList<Response> Responses { get; set; }

        void Prepare(Task task);
        void Send(Task task);
        void AddLogger(ILogger logger);
        Response GetLatestResponse();
    }
}
