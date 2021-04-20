
namespace APITaskManagement.Logic.Api.Data
{
    public class ZwaluwOutboundLine
    {
        public virtual int OrderLineId { get; set; }

        public virtual int SalesOrderHeaderId { get; set; }

        public virtual string ItemCode { get; set; }

        public virtual float Quantity { get; set; }

        public virtual ZwaluwOutboundHeader Outbound { get; set; }
    }
}
