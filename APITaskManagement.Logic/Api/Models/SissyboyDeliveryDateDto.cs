﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class SissyboyDeliveryDateDto
    {
        [JsonProperty("poNumber")]
        public string PoNumber { get; set; }

        [JsonProperty("deliveryDate")]
        public string Date { get; set; }
    }
}
