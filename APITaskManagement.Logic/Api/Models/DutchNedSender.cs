
using Newtonsoft.Json;

namespace APITaskManagement.Logic.Api.Models
{
    public class DutchNedSender
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("display_name")]
        //public string DisplayName { get; set; }

        //[JsonProperty("address")]
        //public string Address { get; set; }

        //[JsonProperty("zip_code")]
        //public string ZipCode { get; set; }

        //[JsonProperty("city")]
        //public string City { get; set; }

        //[JsonProperty("country_code")]
        //public string CountryCode { get; set; }

        //[JsonProperty("email")]
        //public string Email { get; set; }

        //[JsonProperty("cc_email")]
        //public string CcEmail { get; set; }

        //[JsonProperty("service_email")]
        //public string ServiceEmail { get; set; }

        //[JsonProperty("service_phone_number")]
        //public string ServicePhoneNumber { get; set; }

        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }

        [JsonProperty("cc_email")]
        public string CCMailAddress { get; set; }
    }
}
