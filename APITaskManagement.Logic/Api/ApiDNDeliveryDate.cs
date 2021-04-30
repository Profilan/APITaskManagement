using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APITaskManagement.Logic.Api
{
    public class ApiDNDeliveryDate : Api
    {
        private readonly DutchNedDeliveryDateRepository dutchNedDeliveryDateRepository = new DutchNedDeliveryDateRepository();
        private string CountryCode = "NL";

        public ApiDNDeliveryDate(string name) : base(name)
        {

        }

        protected override bool ExecuteBefore(HttpClient client, Request request, Url url)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            // Get Countrycode from url
            var urlParts = url.Address.Split('?');
            CountryCode = HttpUtility.ParseQueryString(urlParts[1]).Get("country_code");
            if (String.IsNullOrEmpty(CountryCode))
            {
                CountryCode = "NL";
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    string cmdText = "DELETE FROM EEK_DISTRIBUTION_DELIVERYDATES_AVAILABLE WHERE Carrier = '999068' AND CountryCode = '" + CountryCode + "'";
                    SqlCommand command = new SqlCommand(cmdText, connection);
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override void ExecutePost(Request request)
        {
            
        }


        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            throw new NotImplementedException();
        }

        protected override IList<ApiMessage> ProcessResponseForTask(string response)
        {
            IList< DutchNedAvailableDeliveryDateDto> availableDates = JsonConvert.DeserializeObject<IList<DutchNedAvailableDeliveryDateDto>>(response);
            IList<ApiMessage> messages = new List<ApiMessage>();

            int itemCount = 0;

            try
            {
                foreach (DutchNedAvailableDeliveryDateDto deliveryDate in availableDates)
                {
                    DutchNedDeliveryDate date = new DutchNedDeliveryDate
                    {
                        DeliveryDate = DateTime.Parse(deliveryDate.Date),
                        Carrier = "999068",
                        CountryCode = CountryCode,
                        Note = deliveryDate.Note
                    };

                    dutchNedDeliveryDateRepository.Insert(date);

                    ++itemCount;
                }

            }
            catch (Exception ex)
            {
                messages.Add(new ApiMessage()
                {
                    Code = 401,
                    Description = "Error in (ProcessResponseForTask): " + ex.Message
                });
            }

            if (messages.Count > 0)
            {
                return messages;
            }
            else
            {
                messages.Add(new ApiMessage()
                {
                    Code = 200,
                    Description = itemCount + " items processed"
                });

                return messages;
            }
        }

        protected override string RequestAcknowledgement()
        {
            throw new NotImplementedException();
        }
    }
}
