using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Logging.Interfaces;
using APITaskManagement.Logic.ReceiveSend.Interfaces;
using APITaskManagement.Logic.Schedulers;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace APITaskManagement.Logic.ReceiveSend
{
    public class WayfairLabelsBodyDto
    {
        [JsonProperty("poNumber")]
        public string PurchaseOrderNumber { get; set; }

        [JsonProperty("debnr")]
        public string DebtorNumber { get; set; }
    }

    public class WayfairLabels : ReceiveSendAction
    {
        private readonly GraphQLHttpClient _restClient;
        private readonly QueueRepository _queueRepository;
        private string _token;

        public WayfairLabels(Task task) : base(task)
        {
            _token = GetToken();

            _restClient = new GraphQLHttpClient(_task.Url.Address, new NewtonsoftJsonSerializer());
            _restClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            _queueRepository = new QueueRepository();
        }

        public override void Execute()
        {
            IList<Request> data = new List<Request>();

            var items = _queueRepository.ListByTask(_task.Id, _task.TotalProcessedItems);
            foreach (var item in items)
            {
                var body = JsonConvert.DeserializeObject<WayfairLabelsBodyDto>(item.Body);

                Request request = new Request(item.Id, item.Key, item.Body);

                var parameters = new
                {
                    poNumber = body.PurchaseOrderNumber
                };
                var graphQLRequest = new GraphQLHttpRequest
                {
                    Query = @"
                    mutation register($parameters: RegistrationInput!) {
                        purchaseOrders {
                            register(
                                registrationInput: $parameters
                            ) {
                                id,
                                eventDate,
                                pickupDate,
                                consolidatedShippingLabel {
                                    url,
                                },
                                shippingLabelInfo {
                                    carrier,
                                },
                                purchaseOrder {
                                    poNumber,
                                    shippingInfo {
                                        carrierCode
                                    }
                                }
                            }
                        }
                    }  
                ",
                    Variables = new
                    {
                        parameters = parameters
                    }
                };

                try
                {
                    GraphQLResponse<object> graphQLResponse = _restClient.SendMutationAsync<object>(graphQLRequest).Result;

                    if (graphQLResponse.Errors == null)
                    {

                        var client = new RestClient(string.Format(_task.Url.ExternalUrl + "/v1/shipping_label/{0}", body.PurchaseOrderNumber));
                        var labelResponse = client.Get(
                            new RestRequest(Method.GET)
                                .AddHeader("accept", "application/json")
                                .AddHeader("content-type", "application/json")
                                .AddHeader("authorization", string.Format("Bearer {0}", _token))
                        );

                        foreach (var share in _task.Shares)
                        {
                            File.WriteAllBytes(share.UNCPath + "\\" + body.DebtorNumber + "_" + body.PurchaseOrderNumber + "_UNKNOWN.pdf", labelResponse.RawBytes);
                        }
                    }
                    else
                    {
                        Response response = new Response(400, "Bad Request", JsonConvert.SerializeObject(graphQLResponse.Errors));
                        request.SetResponse(response);
                        LogResponse(request);
                    }
                }
                catch (Exception ex)
                {
                    Response response = new Response(500, "Internal Server Error", JsonConvert.SerializeObject(ex.ToString()));
                    request.SetResponse(response);
                    LogResponse(request);
                }
            }
        }

    }
}
