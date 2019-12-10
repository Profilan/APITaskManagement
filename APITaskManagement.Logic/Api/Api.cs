using APITaskManagement.Logic.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APITaskManagement.Logic.Schedulers;
using System.Configuration;
using APITaskManagement.Logic.Config;
using System.Net.Http;
using System.Net.Http.Headers;
using APITaskManagement.Logic.Logging.Interfaces;
using System.Net;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Common;
using Newtonsoft.Json.Linq;

namespace APITaskManagement.Logic.Api
{
    public abstract class Api : IApi
    {
        protected IDictionary<string, string> Properties { get; set; }

        public IList<Request> Requests { get; protected set; }

        protected IList<ILogger> Loggers { get; set; }

        public string Name { get; set; }

        public int TotalItems { get; set; }

        public Api(string name) : this()
        {
            Name = name;
            TotalItems = 100;
        }

        public Api()
        {
            Properties = new Dictionary<string, string>();
            Requests = new List<Request>();
            Loggers = new List<ILogger>();
        }


        public void SendRequestsToTarget(Common.HttpMethod httpMethod, Url url, Authentication authentication, Task task)
        {
            
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(0, 5, 0);

                switch (authentication.AuthenticationType)
                {
                    case AuthenticationType.Basic:
                        var byteArray = new UTF8Encoding().GetBytes(authentication.Username + ":" + authentication.Password);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                        break;
                    case AuthenticationType.OAuth2:
                        // Create JSON body
                        var jsonString = "{\"grant_type\":\"" + authentication.GrantType + "\","
                            + "\"scope\":\"" + authentication.Scope + "\","
                            + "\"client_id\":\"" + authentication.Username + "\","
                            + "\"client_secret\":\"" + authentication.Password + "\"}";
                        responseMessage = client.PostAsync(authentication.OAuthUrl, new StringContent(jsonString, Encoding.UTF8, "application/json")).Result;
                        var result = responseMessage.Content.ReadAsStringAsync().Result;
                        JToken token = JObject.Parse(result);
                        var accessToken = (string)token.SelectToken("access_token");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                        break;
                    case AuthenticationType.ApiKey:
                        client.DefaultRequestHeaders.Add("apikey", task.Authentication.ApiKey);
                        break;
                }

                Requests = GetRequestsForTask(task.Id);

                HashSet<int> keys = new HashSet<int>();
                foreach (Request request in Requests)
                {
                    // Check if Url is reachable
                    if (HostIsReachable(url.Address))
                    {
                        if (!keys.Contains(request.ReferenceId))
                        {
                            try
                            {
                                switch (httpMethod)
                                {
                                    case Common.HttpMethod.Get:
                                        responseMessage = client.GetAsync(url.Address).Result;
                                        break;
                                    case Common.HttpMethod.Post:
                                        responseMessage = client.PostAsync(url.Address, new StringContent(request.Body, Encoding.UTF8, "application/json")).Result;

                                        break;
                                    case Common.HttpMethod.Put:
                                        responseMessage = client.PutAsync(url.Address, new StringContent(request.Body, Encoding.UTF8, "application/json")).Result;
                                        break;
                                    case Common.HttpMethod.Patch:
                                        responseMessage = client.PutAsync(url.Address, new StringContent(request.Body, Encoding.UTF8, "application/json")).Result;
                                        break;
                                    case Common.HttpMethod.Delete:
                                        responseMessage = client.DeleteAsync(url.Address).Result;
                                        break;
                                    default:
                                        responseMessage = client.GetAsync(url.Address).Result;
                                        break;
                                }

                                var result = responseMessage.Content.ReadAsStringAsync().Result;
                                var statusCode = (int)responseMessage.StatusCode;
                                var description = responseMessage.StatusCode.ToString();

                                var response = new Response(statusCode, description, result);
                                request.SetResponse(response);

                                if (request.ExecPost == true)
                                {
                                    ExecutePost(request);
                                }

                                LogResponse(request, url, task.SPLogger);

                                keys.Add(request.ReferenceId);

                            }
                            catch (Exception e)
                            {
                                var response = new Response(500, "Internal Server Error", "Call to API failed. (" + e.Message + ")");
                                request.SetResponse(response);
                            }
                        }
                    }
                    else
                    {
                        var response = new Response(500, "Internal Server Error", "You tried to connect to a host who does not exist. (" + url.Address + ")");
                        request.SetResponse(response);
                    }

                }
            }
        }

        private bool HostIsReachable(string url)
        {
            IPHostEntry host;
            Uri uri = new Uri(url);

            try
            {
                host = Dns.GetHostEntry(uri.Host);
            }
            catch
            {
                return false;
            }

            return true;
        }


        public void AddLogger(ILogger logger)
        {
            Loggers.Add(logger);
        }

        protected void LogResponse(Request request, Url url, string spLogger)
        {
            foreach (ILogger logger in Loggers)
            {
                logger.Log(request, url, spLogger);
            }
        }

        protected abstract IList<Request> GetRequestsForTask(Guid taskId);

        protected abstract void ExecutePost(Request request);

        public int GetNumberOfRequests()
        {
            return Requests.Count();
        }

        public virtual Response GetLatestResponse()
        {
            if (Requests.Count > 0)
            {
                return Requests.Last().Response;
            }

            return null;
        }
    }
}
