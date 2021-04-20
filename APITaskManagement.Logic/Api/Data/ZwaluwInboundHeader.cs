using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class ZwaluwInboundHeader
    {
        public virtual Guid InboundShipmentHeaderId { get; set; }
        public virtual string Supplier { get; set; }
        public virtual DateTimeOffset DeliveryDate { get; set; }
        public virtual string TruckReference { get; set; }
        public virtual ISet<ZwaluwInboundLine> Lines { get; set; }

        public ZwaluwInboundHeader()
        {
            Lines = new HashSet<ZwaluwInboundLine>();
        }
    }
}
