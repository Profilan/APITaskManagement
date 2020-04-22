using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace APITaskManagement.Web.Models
{
    public class QueueViewModel
    {
        public int Id { get; set; }
        public int Key { get; set; }
        public int TryCount { get; set; }
        public string Title { get; set; }
        public DateTime SysCreated { get; set; }
    }
}