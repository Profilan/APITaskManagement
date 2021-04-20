using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class ChannelEngineOrderLineDto
    {
        [JsonProperty("MerchantProductNo")]
        public string MerchantProductNo { get; set; }

        [JsonProperty("UnitVat")]
        public decimal UnitVat { get; set; }

        [JsonProperty("Quantity")]
        public int Quantity { get; set; }

        [JsonProperty("UnitPriceInclVat")]
        public decimal UnitPriceInclVat { get; set; }

        [JsonProperty("ExpectedDeliveryDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ExpectedDeliveryDate { get; set; }

        [JsonProperty("ExtraData")]
        public IList<ChannelEngineExtraDataDto> ExtraData { get; set; }
    }
}
