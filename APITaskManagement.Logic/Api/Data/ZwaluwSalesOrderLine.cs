using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class ZwaluwSalesOrderLine
    {
        public virtual int OrderLineId { get; set; }
        public virtual string OrderNumber{ get; set; }
        public virtual string OrderLineDescription{ get; set; }
        public virtual int MainItemQuantity{ get; set; }
        public virtual int Quantity{ get; set; }
        public virtual string DeliveryDate{ get; set; }
        public virtual string ItemMainItemCode{ get; set; }
        public virtual string ItemMainItemDescription{ get; set; }
        public virtual string ItemCode{ get; set; }
        public virtual string ItemDescription{ get; set; }
        public virtual string ItemSalesUnit{ get; set; }
        public virtual string ItemEANCode{ get; set; }
        public virtual string ItemBrand{ get; set; }
        public virtual string ItemProductGroup{ get; set; }
        public virtual string ItemColliUnit{ get; set; }
        public virtual float ItemSalesUnitsPerColli{ get; set; }
        public virtual float ItemVolume{ get; set; }
        public virtual float ItemWeight{ get; set; }
        public virtual float ItemHeight{ get; set; }
        public virtual float ItemLength{ get; set; }
        public virtual float ItemWidth{ get; set; }

        [JsonIgnore]
        public virtual ZwaluwSalesOrderHeader SalesOrder { get; set; }

        public ZwaluwSalesOrderLine()
        {

        }
    }
}
