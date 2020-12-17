using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Api.Models
{
    public class PriceSearchResultItemDto
    {
        [JsonProperty("urls")]
        public IList<PriceSearchUrlDto> Urls { get; set; }

        [JsonProperty("ean")]
        public string EAN { get; set; }

        [JsonProperty("lowestPrice")]
        public string LowestPrice { get; set; }
    }
}