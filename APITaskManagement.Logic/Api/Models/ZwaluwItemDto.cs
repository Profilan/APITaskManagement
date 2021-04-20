using Newtonsoft.Json;
using System;

namespace APITaskManagement.Logic.Api.Models
{
    public class ZwaluwItemDto
    {

        [JsonProperty("itemCode")]
        public string ItemCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        [JsonProperty("eanCode")]
        public Int64 EANCode { get; set; }

        [JsonProperty("length")]
        public decimal Length { get; set; }

        [JsonProperty("width")]
        public decimal Width { get; set; }

        [JsonProperty("height")]
        public decimal Height { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        [JsonProperty("weight")]
        public decimal Weight { get; set; }

        [JsonProperty("salesUnitPerColliUnit")]
        public decimal SalesUnitPerColliUnit { get; set; }


        public bool ShouldSerializeSalesUnitPerColliUnit()
        {
            return (SalesUnitPerColliUnit > 0);
        }

        public bool ShouldSerializeEANCode()
        {
            return (EANCode > 0);
        }

        public bool ShouldSerializeWeight()
        {
            return (Weight > 0);
        }

        public bool ShouldSerializeVolume()
        {
            return (Volume > 0);
        }

        public bool ShouldSerializeHeight()
        {
            return (Height > 0);
        }

        public bool ShouldSerializeLength()
        {
            return (Length > 0);
        }

        public bool ShouldSerializeWidth()
        {
            return (Width > 0);
        }
    }
}
