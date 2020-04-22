using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Management.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Services
{
    public class LogService
    {
        public static List<Log> ListByUrlAndStatus(DateTime start, DateTime end, ErrorType errorType)
        {
            var logRepository = new LogRepository();
            var urlRepository = new UrlRepository();

            var items = logRepository.List(start, end, ErrorType.ERR);

            List<Log> logItems = new List<Log>();
            foreach (var logItem in items)
            {
                var url = urlRepository.GetByName(logItem.Url);
                if (url != null)
                {
                    if (url.MonitorInactivity == true)
                    {
                        logItems.Add(logItem);
                    }
                }
                else
                {
                    logItems.Add(logItem);
                }
            }

            return logItems;
        }
    }
}
