using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITaskManagement.Web.Models
{
    public class TaskApiModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("queued")]
        public int Queued { get; set; }

        [JsonProperty("last_run_time")]
        public DateTime LastRunTime { get; set; }

        [JsonProperty("last_run_details")]
        public string LastRunDetails { get; set; }

        [JsonProperty("last_run_result")]
        public string LastRunResult { get; set; }
    }
}