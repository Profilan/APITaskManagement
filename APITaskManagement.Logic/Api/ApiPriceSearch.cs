using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using Newtonsoft.Json;
using NHibernate.Proxy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api
{
    public class ApiPriceSearch : Api
    {

        private readonly PriceSearchRepository priceSearchRepository = new PriceSearchRepository();

        public ApiPriceSearch(string name) : base(name)
        {
            
        }

        protected override bool ExecuteBefore(HttpClient client, Request request, Url url)
        {
            throw new NotImplementedException();
        }

        protected override void ExecutePost(Request request)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            var requests = new List<Request>();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                DataSet dataSet = new DataSet();
                SqlCommand command = new SqlCommand("EEK_sp_PRICESEARCH_CHECK", connection);
                command.CommandTimeout = 300;
                command.CommandType = CommandType.StoredProcedure;
                
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dataSet);
            }
        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            throw new NotImplementedException();
        }

        protected override IList<ApiMessage> ProcessResponseForTask(string response)
        {
            PriceSearchDto priceSearchDto = JsonConvert.DeserializeObject<PriceSearchDto>(response);
            IList<ApiMessage> messages = new List<ApiMessage>();

            int itemCount = 0;

            try
            {
                foreach (PriceSearchResultItemDto resultItem in priceSearchDto.ResultItems)
                {
                    if (resultItem.Urls != null)
                    {
                        foreach (PriceSearchUrlDto url in resultItem.Urls)
                        {
                            try
                            {
                                PriceSearch priceSearch = new PriceSearch();
                                if (url.LastChecked == "-")
                                {
                                    priceSearch.EAN = resultItem.EAN;
                                    priceSearch.LowestPrice = resultItem.LowestPrice;
                                    priceSearch.BaseUrl = url.BaseUrl;
                                    priceSearch.Price = url.Price;
                                    priceSearch.Url = url.Url;
                                    priceSearch.LastChecked = DateTime.Parse("2000-01-01 00:00:00");
                                }
                                else
                                {
                                    priceSearch.EAN = resultItem.EAN;
                                    priceSearch.LowestPrice = resultItem.LowestPrice;
                                    priceSearch.BaseUrl = url.BaseUrl;
                                    priceSearch.Price = url.Price;
                                    priceSearch.Url = url.Url;
                                    priceSearch.LastChecked = DateTime.Parse(url.LastChecked);
                                }
                                priceSearchRepository.Insert(priceSearch);
                                ++itemCount;
                            }
                            catch (Exception ex)
                            {
                                messages.Add(new ApiMessage()
                                {
                                    Code = 401,
                                    Description = "Error in url: " + resultItem.EAN + ": " + ex.Message
                                });
                            }
                        }
                    }
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
