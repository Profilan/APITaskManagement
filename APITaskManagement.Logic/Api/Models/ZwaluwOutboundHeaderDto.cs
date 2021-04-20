using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Api.Models
{
    public class ZwaluwOutboundHeaderDto
    {
        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("salesOrderHeaderId")]
        public int SalesOrderHeaderId { get; set; }

        [JsonProperty("barCode")]
        public string BarCode { get; set; }

        [JsonProperty("debtorNumber")]
        public string DebtorNumber { get; set; }

        [JsonProperty("debtorName")]
        public string DebtorName { get; set; }

        [JsonProperty("deliveryAddress")]
        public string DeliveryAddress { get; set; }

        [JsonProperty("deliveryZip")]
        public string DeliveryZip { get; set; }

        [JsonProperty("deliveryCity")]
        public string DeliveryCity { get; set; }

        [JsonProperty("deliveryCountryCode")]
        public string DeliveryCountryCode { get; set; }

        [JsonProperty("deliveryNote")]
        public string DeliveryNote { get; set; }

        [JsonProperty("deliveryDate")]
        public DateTime DeliveryDate  { get; set; }

        [JsonProperty("carrier")]
        public string Carrier { get; set; }

        [JsonProperty("orderLines")]
        public IList<ZwaluwOutboundLineDto> Lines { get; set; }
    }
}
