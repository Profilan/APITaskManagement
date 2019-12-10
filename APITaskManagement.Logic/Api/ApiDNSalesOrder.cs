using System;
using System.Collections.Generic;
using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Api.Repositories;
using APITaskManagement.Logic.Common.Repositories;
using APITaskManagement.Logic.Schedulers;
using Newtonsoft.Json;

namespace APITaskManagement.Logic.Api
{
    public class DNResponseResult
    {
        [JsonProperty("id")]
        public string OrderlineId { get; set; }

        [JsonProperty("status")]
        public int StatusCode { get; set; }
    }

    public class DNResponse
    {
        [JsonProperty("status")]
        public string StatusDescription { get; set; }

        [JsonProperty("results")]
        public IList<DNResponseResult> Results { get; set; }
    }

    public class ApiDNSalesOrder : Api
    {
        private readonly QueueRepository _queueRepository;
        private readonly DutchNedInsertedOrderlinestatusRepository _statusRepository = new DutchNedInsertedOrderlinestatusRepository();
        private readonly CashOnDeliveryOrderlineRepository _cashOnDeliveryOrderlineRepository = new CashOnDeliveryOrderlineRepository();

        public ApiDNSalesOrder(string name) : base(name)
        {
            _queueRepository = new QueueRepository();
        }

        public ApiDNSalesOrder() : base()
        {
            _queueRepository = new QueueRepository();
        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            var requests = new List<Request>();
            var items = _queueRepository.ListByTask(taskId, TotalItems);

            var formatter = new DNSalesOrderFormatter();

            foreach (var item in items)
            {
                var content = formatter.GetJsonContent(item.Key, Properties);

                if (!String.IsNullOrEmpty(content))
                {
                    var request = new Request(item.Id, (int)item.Key, content, true);

                    requests.Add(request);
                }
            }

            return requests;
        }

        protected override void ExecutePost(Request request)
        {
            try
            {
                // Get json 
                var jsonString = request.Response.Detail;

                var response = JsonConvert.DeserializeObject<DNResponse>(jsonString);

                foreach (var result in response.Results)
                {
                    string description = "Unknown";
                    switch (result.StatusCode)
                    {
                        case 600:
                            description = "Imported new";
                            break;
                        case 601:
                            description = "Imported and automatically planned based on other delivery-order";
                            break;
                        case 602:
                            description = "Imported added to another delivery-order";
                            break;
                        case 603:
                            description = "Updated";
                            break;
                        case 604:
                            description = "Can’t be changed already planned";
                            break;
                    }
                    var status = new DutchNedInsertedOrderlinestatus()
                    {
                        Status = result.StatusCode,
                        Description = description,
                        OrderLineId = Convert.ToInt32(result.OrderlineId)
                    };

                    _statusRepository.Insert(status);

                }
                try
                {
                    if (request.Response.Code == 200)
                    {
                        APITaskManagement.Logic.Api.Models.DutchNedSalesOrder salesOrder = JsonConvert.DeserializeObject<APITaskManagement.Logic.Api.Models.DutchNedSalesOrder>(request.Body);

                        foreach (var line in salesOrder.Lines)
                        {
                            var cashOnDelivery = new CashOnDeliveryOrderline()
                            {
                                OrderlineId = Convert.ToInt32(line.Id),
                                CashOnDelivery = line.CashOnDelivery
                            };

                            _cashOnDeliveryOrderlineRepository.Insert(cashOnDelivery);
                        }
                    }
                }
                catch
                {

                }
            }
            catch
            {

            }
        }

        public void TestExecutePost(Request request)
        {
            ExecutePost(request);
        }

    }
}
