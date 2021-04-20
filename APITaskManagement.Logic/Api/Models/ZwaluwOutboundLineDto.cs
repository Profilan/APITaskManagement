using Newtonsoft.Json;

namespace APITaskManagement.Logic.Api.Models
{
    public class ZwaluwOutboundLineDto
    {
        [JsonProperty("orderLineId")]
        public int OrderLineId { get; set; }

        [JsonProperty("itemCode")]
        public string ItemCode { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
    }
}
