using Newtonsoft.Json;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Api.Models
{
    public class DutchNedSalesOrder
    {
        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("order_date")]
        public string OrderDate { get; set; }

        [JsonProperty("order_reference")]
        public string OrderReference { get; set; }

        [JsonProperty("delivery_date")]
        public string DeliveryDate { get; set; }

        [JsonProperty("delivery_instructions")]
        public string DeliveryInstructions { get; set; }

        [JsonProperty("preferred_delivery_time_slot")]
        public string PreferredDeliveryTimeSlot { get; set; }

        [JsonProperty("is_comby_order")]
        public bool IsCombyOrder { get; set; }

        [JsonProperty("mail_customer")]
        public bool MailCustomer { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_street")]
        public string CustomerStreet { get; set; }

        [JsonProperty("customer_house_number")]
        public string CustomerHouseNumber { get; set; }

        [JsonProperty("customer_house_number_addition")]
        public string CustomerHouseNumberAddition { get; set; }

        [JsonProperty("customer_zip_code")]
        public string CustomerZipCode { get; set; }


        [JsonProperty("customer_city")]
        public string CustomerCity { get; set; }

        [JsonProperty("customer_country_code")]
        public string CustomerCountryCode { get; set; }

        [JsonProperty("customer_email")]
        public string CustomerEmail { get; set; }

        [JsonProperty("customer_phone_number")]
        public string CustomerPhoneNumber { get; set; }

        [JsonProperty("customer_instructions")]
        public string CustomerInstructions { get; set; }

        [JsonProperty("sender")]
        public DutchNedSender Sender { get; set; }

        [JsonProperty("order_lines")]
        public IList<DutchNedSalesOrderLine> Lines { get; set; }

        public DutchNedSalesOrder()
        {
            Lines = new List<DutchNedSalesOrderLine>();
        }
    }
}
