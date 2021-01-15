using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class ChannelEngineOrderHeaderDto
    {
        [JsonProperty]
        public int Id { get; set; }

        [JsonProperty("ChannelName")]
        public string ChannelName { get; set; }

        [JsonProperty("ChannelId")]
        public int ChannelId { get; set; }

        [JsonProperty("ChannelOrderNo")]
        public string ChannelOrderNo { get; set; }

        [JsonProperty("MerchantComment")]
        public string MerchantComment { get; set; }

        [JsonProperty("ShippingAddress")]
        public ChannelEngineAddressDto ShippingAddress { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("ChannelCustomerNo")]
        public string ChannelCustomerNo { get; set; }

        [JsonProperty("Lines")]
        public IList<ChannelEngineOrderLineDto> Lines { get; set; }
    }
}
