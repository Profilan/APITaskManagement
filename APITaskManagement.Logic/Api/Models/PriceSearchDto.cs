using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class PriceSearchDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("result")]
        public IList<PriceSearchResultItemDto> ResultItems { get; set; }
    }
}
