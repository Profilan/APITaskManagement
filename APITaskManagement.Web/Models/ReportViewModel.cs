using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITaskManagement.Web.Models
{
    public class ReportViewModel
    {
        public IEnumerable<Log> Logs { get; set; }
        public int NumberOfInfoMessages { get { return Logs.Count(log => log.PriorityName == "INFO"); } }
        public int NumberOfWarningMessages { get { return Logs.Count(log => log.PriorityName == "WARN"); } }
        public int NumberOfErrorMessages { get { return Logs.Count(log => log.PriorityName == "ERR"); } }
    }
}