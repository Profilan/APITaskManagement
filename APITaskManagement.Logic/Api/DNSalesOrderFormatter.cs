using System;
using System.Collections.Generic;
using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using Newtonsoft.Json;

namespace APITaskManagement.Logic.Api
{
    public class DNSalesOrderFormatter : IContentFormatter
    {
        private readonly DutchNedSalesOrderRepository _salesOrderRepository = new DutchNedSalesOrderRepository();

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            try
            {
                var salesOrder = _salesOrderRepository.GetById(key);

                if (salesOrder != null && salesOrder.Lines.Count > 0)
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
                            LogoUrl = salesOrder.Sender.LogoUrl
                            
                        },
                    };

                    foreach (var line in salesOrder.Lines)
                    {
                        var salesOrderLineView = new APITaskManagement.Logic.Api.Models.DutchNedSalesOrderLineDto()
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
                            Type = line.Type
                        };
                        salesOrderView.Lines.Add(salesOrderLineView);
                    }

                    return JsonConvert.SerializeObject(salesOrderView);
                }
            }
            catch (Exception e)
            {
                return "[Error]:[" + e.Message + "]";
            }

            return null;
        }
    }
}
