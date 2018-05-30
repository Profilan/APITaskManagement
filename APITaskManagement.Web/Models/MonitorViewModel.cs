using APITaskManagement.Logic.Monitoring;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APITaskManagement.Web.Models
{
    public class MonitorViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Enabled { get; set; }

        public IEnumerable<Messenger> SelectedMessengers { get; set; }
        public IEnumerable<Messenger> Messengers { get; set; }
    }
}