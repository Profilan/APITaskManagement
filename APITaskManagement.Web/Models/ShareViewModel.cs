using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APITaskManagement.Web.Models
{
    public class ShareViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UNCPath { get; set; }
    }
}