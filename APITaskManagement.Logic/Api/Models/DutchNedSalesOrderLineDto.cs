
using Newtonsoft.Json;

namespace APITaskManagement.Logic.Api.Models
{
    public class DutchNedSalesOrderLineDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("collection_date")]
        public string CollectionDate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ean_code")]
        public string EANCode { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("main_package_identifier")]
        public string MainPackageIdentifier { get; set; }

        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("plan_from_date")]
        public string PlanFromDate { get; set; }

        [JsonProperty("warehouse")]
        public string Warehouse { get; set; }

        [JsonProperty("cash_on_delivery")]
        public int CashOnDelivery { get; set; }

        [JsonProperty("return")]
        public bool IsReturn { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
