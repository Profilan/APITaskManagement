using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITaskManagement.Web.Models
{
    public class SortAndPage
    {
        public string SortField { get; set; }
        public string SortDirection { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }
    }
}