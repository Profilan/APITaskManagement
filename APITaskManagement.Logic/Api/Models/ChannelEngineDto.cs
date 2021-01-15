using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class ChannelEngineDto
    {
        [JsonProperty("Content")]
        public IList<ChannelEngineOrderHeaderDto> Content { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

        [JsonProperty("TotalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("ItemsPerPage")]
        public int ItemsPerPage { get; set; }

        [JsonProperty("StatusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
