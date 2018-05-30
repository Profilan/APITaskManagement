using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITaskManagement.Web.Models
{
    public class LogViewModel
    {
        public virtual DateTime TimeStamp { get; set; }

        public virtual int Priority { get; set; }

        public virtual string Message { get; set; }

        public virtual string PriorityName { get; set; }

        public virtual string Url { get; set; }

        public virtual string Detail { get; set; }

        public virtual ErrorType ErrorType { get; set; }
    }
}