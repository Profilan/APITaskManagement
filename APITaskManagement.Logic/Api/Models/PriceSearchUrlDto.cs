using Newtonsoft.Json;

namespace APITaskManagement.Logic.Api.Models
{
    public class PriceSearchUrlDto
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("lastChecked")]
        public string LastChecked { get; set; }
    }
}