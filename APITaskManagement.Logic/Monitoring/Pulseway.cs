using APITaskManagement.Logic.Monitoring.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM.Monitor.Client;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace APITaskManagement.Logic.Monitoring
{
    public class Pulseway : Messenger
    {
        public Pulseway() : base()
        {
            Enabled = Convert.ToBoolean(ConfigurationManager.AppSettings["PulsewayEnabled"]);
        }

        public override void Send(string subject, string body)
        {
            var username = ConfigurationManager.AppSettings["PulsewayUsername"];
            var password = ConfigurationManager.AppSettings["PulsewayPassword"];
            var endpoint = ConfigurationManager.AppSettings["PulsewayEndpoint"];
            var instanceId = ConfigurationManager.AppSettings["PulsewayInstanceId"];

            using (HttpClient client = new HttpClient())
            {
                var byteArray = new UTF8Encoding().GetBytes(username + ":" + password);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                // Create JSON body
                var jsonString = "{\"instance_id\":\"" + instanceId + "\","
                    + "\"title\":\"" + subject + "\","
                    + "\"message\":\"" + body + "\","
                    + "\"priority\":\"critical\"}";

                var responseMessage = client.PostAsync(endpoint, new StringContent(jsonString, Encoding.UTF8, "application/json")).Result;
                var result = responseMessage.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
