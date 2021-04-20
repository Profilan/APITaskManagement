using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class ZwaluwInboundLine
    {
        public virtual Guid Id { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual string PurchaseOrderNumber { get; set; }
        public virtual int CustomerLineId { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual ZwaluwInboundHeader Header { get; set; }

    }
}
