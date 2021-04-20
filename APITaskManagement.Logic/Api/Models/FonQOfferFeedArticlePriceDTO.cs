using Newtonsoft.Json;

namespace APITaskManagement.Logic.Api.Models
{
    public class FonQOfferFeedArticlePriceDTO
    {
        [JsonProperty("unitPrice")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("recommendedRetailPrice")]
        public decimal RecommendedRetailPrice { get; set; }

        [JsonProperty("priceCurrency")]
        public string PriceCurrency { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }
    }
}
