using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class DutchNedAvailableDeliveryDateDto
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }
    }
}
