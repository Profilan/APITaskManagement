using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class ChannelEngineAcknowledgeDto
    {
        [JsonProperty("MerchantOrderNo")]
        public string MerchantOrderNo { get; set; }

        [JsonProperty("OrderId")]
        public int OrderId { get; set; }
    }
}
