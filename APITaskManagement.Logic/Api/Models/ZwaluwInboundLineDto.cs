using Newtonsoft.Json;
using System;

namespace APITaskManagement.Logic.Api.Models
{
    public class ZwaluwInboundLineDto
    {
        [JsonProperty("orderLineId")]
        public int OrderLineId { get; set; }

        [JsonProperty("itemCode")]
        public string ItemCode { get; set; }

        [JsonProperty("purchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
    }
}
