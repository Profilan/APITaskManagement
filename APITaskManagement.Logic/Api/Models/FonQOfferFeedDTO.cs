using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class FonQOfferFeedDTO
    {
        [JsonProperty("$schema")]
        public string Schema { get; set; }

        [JsonProperty("ean")]
        public string EAN { get; set; }

        [JsonProperty("moq")]
        public int MOQ { get; set; }

        [JsonProperty("articleStatus")]
        public string ArticleStatus { get; set; }
        
        [JsonProperty("vatPercentage")]
        public string VATPercentage { get; set; }

        [JsonProperty("brandSupplier")]
        public string BrandSupplier { get; set; }

        [JsonProperty("articleDescription")]
        public string ArticleDescription { get; set; }

        [JsonProperty("articleNumberSupplier")]
        public string ArticleNumberSupplier { get; set; }

        [JsonProperty("fragile")]
        public bool Fragile { get; set; }

        [JsonProperty("deliveryLeadTime")]
        public int DeliveryLeadTime { get; set; }

        [JsonProperty("weight")]
        public decimal Weight { get; set; }

        [JsonProperty("length")]
        public decimal Length { get; set; }

        [JsonProperty("width")]
        public decimal Width { get; set; }

        [JsonProperty("height")]
        public decimal Height { get; set; }

        [JsonProperty("quantityPerBox")]
        public int QuantityPerBox { get; set; }

        [JsonProperty("quantityPerOuterCarton")]
        public int QuantityPerOuterCarton { get; set; }

        [JsonProperty("quantityPerPallet")]
        public int QuantityPerPallet { get; set; }

        [JsonProperty("quantityFreeOnStock")]
        public int QuantityFreeOnStock { get; set; }

        [JsonProperty("restockDate", NullValueHandling = NullValueHandling.Ignore)]
        public string RestockDate { get; set; }

        [JsonProperty("articlePrice")]
        public IList<FonQOfferFeedArticlePriceDTO> Prices { get; set; }

        public bool ShouldSerializeQuantityPerPallet()
        {
            return (QuantityPerPallet > 0);
        }
    }
}
