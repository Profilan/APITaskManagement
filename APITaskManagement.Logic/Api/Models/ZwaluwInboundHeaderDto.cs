using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class ZwaluwInboundHeaderDto
    {
        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        [JsonProperty("truckReference")]
        public string TruckReference { get; set; }

        [JsonProperty("deliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [JsonProperty("orderLines")]
        public IList<ZwaluwInboundLineDto> Lines { get; set; }
    }
}
