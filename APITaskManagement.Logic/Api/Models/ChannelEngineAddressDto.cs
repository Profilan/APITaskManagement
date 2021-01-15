using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class ChannelEngineAddressDto
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("StreetName")]
        public string StreetName { get; set; }

        [JsonProperty("HouseNr")]
        public string HouseNr { get; set; }

        [JsonProperty("HouseNrAddition")]
        public string HouseNrAddition { get; set; }

        [JsonProperty("ZipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("CountryIso")]
        public string CountryIso { get; set; }
    }
}
