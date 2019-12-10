using APITaskManagement.Logic.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Schedulers.Repositories;
using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Management.Repositories;
using APITaskManagement.Logic.Filer;
using APITaskManagement.Logic.Filer.Data;

namespace APITaskManagement.Logic.Logging
{
    public class SystemLogger : ILogger
    {
        public void Log(Request request, Url url, string spLogger)
        {
            var _logRepository = new LogRepository();

            var priority = ErrorType.INFO;
            if (request.Response.Code >= 400)
            {
                priority = ErrorType.ERR;
            }

            var detail = "{\"key\": " + request.ReferenceId + ",\"request\": " + request.Body + ",\"response\":" + request.Response.Detail + "}";
            var message = request.Response.Code + " " + request.Response.Description;
            var log = new Log(DateTime.Now, (int)priority, message, Enum.GetName(typeof(ErrorType), (int)priority), url.Address, detail, false);

            _logRepository.Insert(log);
        }

        public void Log(Response response, Share share)
        {
            var _logRepository = new LogRepository();

            var priority = ErrorType.INFO;
            if (response.Code >= 400)
            {
                priority = ErrorType.ERR;
            }

            var detail = response.Detail;
            var message = response.Code + " " + response.Description;

            var log = new Log(DateTime.Now, (int)priority, message, Enum.GetName(typeof(ErrorType), (int)priority), share.UNCPath, detail, false);

            _logRepository.Insert(log);
        }

        public void Log(Response response, string recipient, string spLogger)
        {
            var _logRepository = new LogRepository();

            var priority = ErrorType.INFO;
            if (response.Code >= 400)
            {
                priority = ErrorType.ERR;
            }

            var detail = response.Detail;
            var message = response.Code + " " + response.Description;

            var log = new Log(DateTime.Now, (int)priority, message, Enum.GetName(typeof(ErrorType), (int)priority), recipient, detail, false);

            _logRepository.Insert(log);
        }
    }
}
