using Newtonsoft.Json;
using System;

namespace APITaskManagement.Logic.Api.Models
{
    public class ZwaluwIdentifierDto
    {
        [JsonProperty("InboundShipmentHeaderId")]
        public Guid InboundShipmentHeaderId { get; set; }
    }
}
