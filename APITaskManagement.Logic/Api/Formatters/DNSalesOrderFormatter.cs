using System;
using System.Collections.Generic;
using System.Linq;
using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using Newtonsoft.Json;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class DNSalesOrderFormatter : IContentFormatter
    {
        private readonly DutchNedSalesOrderRepository _salesOrderRepository = new DutchNedSalesOrderRepository();
        private readonly DutchNedSalesOrderLineRepository _salesOrderLineRepository = new DutchNedSalesOrderLineRepository();

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            try
            {
                var salesOrder = _salesOrderRepository.GetById(key);

                var salesOrderLines = _salesOrderLineRepository.GetLinesBySalesOrderHeaderId(salesOrder.Id);

                if (salesOrder != null)
                {
                    var deliveryDate = salesOrder.DeliveryDate.ToString("yyyy-MM-dd");
                    var salesOrderView = new DutchNedSalesOrderDto()
                    {
                        OrderNumber = salesOrder.OrderNumber,
                        OrderDate = salesOrder.OrderDate.ToString("yyyy-MM-dd"),
                        OrderReference = salesOrder.OrderReference,
                        DeliveryInstructions = salesOrder.DeliveryInstructions,
                        DeliveryDate = deliveryDate == "0001-01-01" ? null : deliveryDate,
                        PreferredDeliveryTimeSlot = salesOrder.PreferredDeliveryTimeSlot,
                        IsCombyOrder = salesOrder.IsCombyOrder,
                        MailCustomer = salesOrder.MailCustomer,
                        CustomerName = salesOrder.Customer.Name,
                        CustomerStreet = salesOrder.Customer.Street,
                        CustomerHouseNumber = salesOrder.Customer.HouseNumber,
                        CustomerHouseNumberAddition = salesOrder.Customer.HouseNumberAddition,
                        CustomerZipCode = salesOrder.Customer.ZipCode,
                        CustomerCity = salesOrder.Customer.City,
                        CustomerCountryCode = salesOrder.Customer.CountryCode,
                        CustomerEmail = salesOrder.Customer.Email,
                        CustomerPhoneNumber = salesOrder.Customer.PhoneNumber,
                        CustomerInstructions = salesOrder.Customer.Instructions,
                        Sender = new DutchNedSender()
                        {
                            Name = salesOrder.Sender.Name,
                            LogoUrl = salesOrder.Sender.LogoUrl,
                            CCMailAddress = salesOrder.Sender.CCMailAddress
                        },
                    };

                    foreach (var line in salesOrderLines)
                    {
                        if (line.PickedUpDate != DateTime.MinValue)
                        {
                            salesOrderView.Lines.Add(new APITaskManagement.Logic.Api.Models.DutchNedSalesOrderLineDto()
                            {
                                Id = line.Id.ToString(),
                                CollectionDate = line.CollectionDate.ToString("yyyy-MM-dd"),
                                Description = line.Description,
                                EANCode = line.EANCode,
                                Identifier = line.Identifier,
                                MainPackageIdentifier = line.MainPackageIdentifier,
                                Volume = line.Volume,
                                Warehouse = line.Warehouse.Trim(),
                                CashOnDelivery = line.CashOnDelivery,
                                IsReturn = line.IsReturn,
                                PlanFromDate = line.PlanFromDate.ToString("yyyy-MM-dd"),
                                Type = line.Type,
                                Height = line.Height,
                                Length = line.Length,
                                Width = line.Width,
                                Weight = line.Weight,
                                PickedUpDate = line.PickedUpDate.ToString("yyyy-MM-ddTHH:mm:sszzz")
                            });
                        }
                        else
                        {
                            salesOrderView.Lines.Add(new APITaskManagement.Logic.Api.Models.DutchNedSalesOrderLineDto()
                            {
                                Id = line.Id.ToString(),
                                CollectionDate = line.CollectionDate.ToString("yyyy-MM-dd"),
                                Description = line.Description,
                                EANCode = line.EANCode,
                                Identifier = line.Identifier,
                                MainPackageIdentifier = line.MainPackageIdentifier,
                                Volume = line.Volume,
                                Warehouse = line.Warehouse.Trim(),
                                CashOnDelivery = line.CashOnDelivery,
                                IsReturn = line.IsReturn,
                                PlanFromDate = line.PlanFromDate.ToString("yyyy-MM-dd"),
                                Type = line.Type,
                                Height = line.Height,
                                Length = line.Length,
                                Width = line.Width,
                                Weight = line.Weight,
                                PickedUpDate = null
                            });
                        }
                    }

                    return JsonConvert.SerializeObject(salesOrderView);
                }
            }
            catch (Exception e)
            {
                throw new Exception( "[Error]:[" + e.Message + "]");
            }

            return null;
        }
    }
}
