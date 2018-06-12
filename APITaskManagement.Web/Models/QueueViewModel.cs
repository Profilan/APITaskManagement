using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace APITaskManagement.Web.Models
{
    public class QueueViewModel
    {
        public virtual int Key { get; set; }
        public virtual int TryCount { get; set; }
        public virtual Task Task { get; set; }
    }
}