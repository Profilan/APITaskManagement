using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class ZwaluwSalesOrderFormatter : IContentFormatter
    {
        private readonly ZwaluwSalesOrderRepository zwaluwSalesOrderRepository = new ZwaluwSalesOrderRepository();

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            // Get the salesOrderHeader

            try
            {
                var orderHeader = zwaluwSalesOrderRepository.GetById(key);
                var orderHeaderDto = new ZwaluwSalesOrderDto()
                {
                    CustomerId = orderHeader.CustomerId,
                    DebtorName = orderHeader.DebtorName,
                    DebtorNumber = orderHeader.DebtorNumber,
                    DelAddress = orderHeader.DelAddress,
                    DelCity = orderHeader.DelCity,
                    Carrier = orderHeader.Carrier,
                    DelCountryCode = orderHeader.DelCountryCode,
                    DelZip = orderHeader.DelZip,
                    Description = orderHeader.Description,
                    OrderNumber = orderHeader.OrderNumber,
                    Reference = orderHeader.Reference,
                    WarehouseId = orderHeader.WarehouseId,
                    DeliveryNote = orderHeader.DeliveryNote,
                    TestIndicator = orderHeader.TestIndicator,
                    MainDebtorNumber = orderHeader.MainDebtorNumber
                };

                if (orderHeader != null)
                {
                    if (orderHeader.DelZip == null)
                    {
                        orderHeader.DelZip = "Unknown";
                    }

                    var salesOrderDto = new ZwaluwSalesOrderHeaderDto();
                    salesOrderDto.SalesOrderHeaders.Add(orderHeaderDto);

                    foreach (var orderLine in orderHeader.Lines)
                    {
                        if (orderLine.OrderLineDescription == null)
                        {
                            orderLine.OrderLineDescription = orderLine.ItemMainItemDescription;
                        }


                        var salesOrderLineDto = new ZwaluwSalesOrderLineDto()
                        {
                            OrderNumber = orderLine.OrderNumber,
                            OrderLineDescription = orderLine.OrderLineDescription,
                            MainItemQuantity = orderLine.MainItemQuantity,
                            Quantity = orderLine.Quantity,
                            DeliveryDate = orderLine.DeliveryDate,
                            OrderLineId = orderLine.OrderLineId,
                            ItemMainItemCode = orderLine.ItemMainItemCode,
                            ItemMainItemDescription = orderLine.ItemMainItemDescription,
                            ItemCode = orderLine.ItemCode,
                            ItemDescription = orderLine.ItemDescription,
                            ItemSalesUnit = orderLine.ItemSalesUnit,
                            ItemEANCode = orderLine.ItemEANCode,
                            ItemBrand = orderLine.ItemBrand,
                            ItemProductGroup = orderLine.ItemProductGroup,
                            ItemColliUnit = orderLine.ItemColliUnit,
                            ItemSalesUnitsPerColli = orderLine.ItemSalesUnitsPerColli,
                            ItemVolume = orderLine.ItemVolume,
                            ItemWeight = orderLine.ItemWeight,
                            ItemHeight = orderLine.ItemHeight,
                            ItemLength = orderLine.ItemLength,
                            ItemWidth = orderLine.ItemWidth
                        };
                        salesOrderDto.SalesOrderLines.Add(salesOrderLineDto);
                    }

                    return new JavaScriptSerializer().Serialize(salesOrderDto);
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }

        }
    }
}
