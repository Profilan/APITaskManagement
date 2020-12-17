using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PriceSearch : Entity<int>
    {
        public virtual string EAN { get; set; }

        public virtual string LowestPrice { get; set; }

        public virtual string BaseUrl { get; set; }

        public virtual string Price { get; set; }

        public virtual string Url { get; set; }

        public virtual DateTime? LastChecked { get; set; }

    }
}
