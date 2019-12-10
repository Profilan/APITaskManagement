using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APITaskManagement.Web.Models
{
    public class UrlViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [Display(Name = "External Url")]
        public string ExternalUrl { get; set; }

        public int Amount { get; set; }

        public Unit Unit { get; set; }
        [Display(Name = "Monitor Inactivity")]
        public bool MonitorInactivity { get; set; }
    }
}