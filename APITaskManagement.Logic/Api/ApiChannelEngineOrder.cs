using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using APITaskManagement.Logic.Management;
using APITaskManagement.Logic.Schedulers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api
{
    public class ApiChannelEngineOrder : Api
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public ApiChannelEngineOrder(string name) : base(name)
        {

        }

        protected override bool ExecuteBefore(HttpClient client, Request request, Url url)
        {
            throw new NotImplementedException();
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
            
            ChannelEngineDto dto = JsonConvert.DeserializeObject<ChannelEngineDto>(response);
            IList<ApiMessage> messages = new List<ApiMessage>();

            int itemCount = 0;

            try
            {
                if (dto.Count > 0)
                {
                    foreach (ChannelEngineOrderHeaderDto order in dto.Content)
                    {
                        string debiteurnr = order.ChannelName.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                        OrderIdentifier identifier = new OrderIdentifier(order.ChannelOrderNo, debiteurnr);

                        // JsonConvert.SerializeObject(order);

                        // Check if the order already exists
                        var existingOrder = orderRepository.GetByIdentifier(identifier);

                        if (existingOrder.Count() == 0)
                        {

                            string shippingAddress = order.ShippingAddress.StreetName;
                            if (!string.IsNullOrEmpty(order.ShippingAddress.HouseNr))
                            {
                                shippingAddress += " " + order.ShippingAddress.HouseNr;
                            }
                            if (!string.IsNullOrEmpty(order.ShippingAddress.HouseNrAddition))
                            {
                                shippingAddress += " " + order.ShippingAddress.HouseNrAddition;
                            }

                            OrderHeader orderHeader = new OrderHeader
                            {
                                OrderIdentifier = identifier,
                                ORDERTOELICHTING = order.MerchantComment,
                                DS_NAAM = order.ShippingAddress.FirstName + " " + order.ShippingAddress.LastName,
                                DS_ADRES1 = shippingAddress,
                                DS_POSTCODE = order.ShippingAddress.ZipCode,
                                DS_PLAATS = order.ShippingAddress.City,
                                DS_LAND = order.ShippingAddress.CountryIso,
                                DS_TELEFOON = order.Phone,
                                DS_EMAIL = order.Email,
                                AUTHENTICATED_USER = "ChannelEngineApi",
                                CUSTOMERORDERID = order.Id,
                                SELECTIECODE = "TH"
                            };

                            foreach (ChannelEngineOrderLineDto line in order.Lines)
                            {
                                var nettoPrijs = line.UnitPriceInclVat - line.UnitVat;

                                bool exactDeliveryDate = false;
                                if (line.ExtraData.Count() > 0)
                                {
                                    foreach (var data in line.ExtraData)
                                    {
                                        if (data.Key == "ExactDeliveryDate")
                                        {
                                            if (data.Value == "True")
                                            {
                                                exactDeliveryDate = true;
                                            }
                                        }
                                    }
                                }
                                
                                if (line.ExpectedDeliveryDate != DateTime.MinValue && exactDeliveryDate == true)
                                {
                                    orderHeader.Lines.Add(new OrderLine
                                    {
                                        ITEMCODE = line.MerchantProductNo,
                                        NETTO_PRIJS = (float)nettoPrijs,
                                        NETT_PRICE_INCL_VAT = line.UnitPriceInclVat,
                                        UNIT_VAT = line.UnitVat,
                                        AANTAL = line.Quantity,
                                        REQUESTEDDATE = line.ExpectedDeliveryDate,
                                        OrderIdentifier = identifier
                                    });
                                }
                                else
                                {
                                    orderHeader.Lines.Add(new OrderLine
                                    {
                                        ITEMCODE = line.MerchantProductNo,
                                        NETTO_PRIJS = (float)nettoPrijs,
                                        NETT_PRICE_INCL_VAT = line.UnitPriceInclVat,
                                        UNIT_VAT = line.UnitVat,
                                        AANTAL = line.Quantity,
                                        REQUESTEDDATE = null,
                                        OrderIdentifier = identifier
                                    });
                                }
                            }

                            orderRepository.Insert(orderHeader);

                            ++itemCount;
                        }
                    }
                }
                else
                {
                    messages.Add(new ApiMessage
                    {
                        Code = 200,
                        Description = "No orders to process"
                    });
                }
            }
            catch (Exception ex)
            {
                messages.Add(new ApiMessage()
                {
                    Code = 401,
                    Description = "Error in (ProcessResponseForTask): " + ex.ToString()
                });
            }

            if (messages.Count > 0) // Means that an error was raised
            {
                return messages;
            }
            else
            {
                messages.Add(new ApiMessage()
                {
                    Code = 200,
                    Description = itemCount + " items processed: " + response
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
