using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APITaskManagement.Web.Models
{
    public class SchedulerViewModel
    {
        [Required]
        public string Tag { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime TriggerTime { get; set; }

        public bool Enabled { get; set; }
    }
}