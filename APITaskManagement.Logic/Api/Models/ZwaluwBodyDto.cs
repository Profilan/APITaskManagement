using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class ZwaluwBodyDto
    {
        [JsonProperty("DeliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [JsonProperty("ediReference")]
        public string EdiReference { get; set; }

        [JsonProperty("apiType")]
        public string ApiType { get; set; }
    }
}
