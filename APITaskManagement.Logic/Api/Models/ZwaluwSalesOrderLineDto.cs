using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class ZwaluwSalesOrderLineDto
    {
        public int OrderLineId { get; set; }
        public string OrderNumber { get; set; }
        public string OrderLineDescription { get; set; }
        public int MainItemQuantity { get; set; }
        public int Quantity { get; set; }
        public string DeliveryDate { get; set; }
        public string ItemMainItemCode { get; set; }
        public string ItemMainItemDescription { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ItemSalesUnit { get; set; }
        public string ItemEANCode { get; set; }
        public string ItemBrand { get; set; }
        public string ItemProductGroup { get; set; }
        public string ItemColliUnit { get; set; }
        public float ItemSalesUnitsPerColli { get; set; }
        public float ItemVolume { get; set; }
        public float ItemWeight { get; set; }
        public float ItemHeight { get; set; }
        public float ItemLength { get; set; }
        public float ItemWidth { get; set; }
    }
}
