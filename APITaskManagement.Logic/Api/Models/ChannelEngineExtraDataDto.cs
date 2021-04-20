using Newtonsoft.Json;

namespace APITaskManagement.Logic.Api.Models
{
    public class ChannelEngineExtraDataDto
    {
        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}
