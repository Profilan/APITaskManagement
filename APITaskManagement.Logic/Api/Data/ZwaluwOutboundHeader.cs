
using System.Collections.Generic;

namespace APITaskManagement.Logic.Api.Data
{
    public class ZwaluwOutboundHeader
    {
        public virtual string OrderNumber { get; set; }

        public virtual int SalesOrderHeaderId { get; set; }

        public virtual string Barcode { get; set; }

        public virtual string DebtorNumber { get; set; }

        public virtual string DebtorName { get; set; }

        public virtual string DeliveryAddress { get; set; }

        public virtual string DeliveryZip { get; set; }

        public virtual string DeliveryCity { get; set; }

        public virtual string DeliveryCountryCode { get; set; }

        public virtual string DeliveryNote { get; set; }

        public virtual string Carrier { get; set; }

        public virtual ISet<ZwaluwOutboundLine> Lines { get; set; }

        public ZwaluwOutboundHeader()
        {
            Lines = new HashSet<ZwaluwOutboundLine>();
        }
    }
}
