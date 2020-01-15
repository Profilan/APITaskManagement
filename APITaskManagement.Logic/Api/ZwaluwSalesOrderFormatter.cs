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

namespace APITaskManagement.Logic.Api
{
    public class ZwaluwSalesOrderFormatter : IContentFormatter
    {
        private readonly ZwaluwSalesOrderRepository zwaluwSalesOrderRepository = new ZwaluwSalesOrderRepository();

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            // Get the salesOrderHeader
            var orderHeader = zwaluwSalesOrderRepository.GetById(key);

            if (orderHeader != null)
            {
                if (orderHeader.DelZip == null)
                {
                    orderHeader.DelZip = "Unknown";
                }

                var salesOrderDto = new ZwaluwSalesOrderDto();
                salesOrderDto.SalesOrderHeaders.Add(orderHeader);

                foreach (var orderLine in orderHeader.Lines)
                {
                    if (orderLine.OrderLineDescription == null)
                    {
                        orderLine.OrderLineDescription = orderLine.ItemMainItemDescription;
                    }

                    salesOrderDto.SalesOrderLines.Add(orderLine);
                }

                return new JavaScriptSerializer().Serialize(salesOrderDto);
            }
            else
            {
                return null;
            }

            /*
            string connectionstring = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                connection.Open();

                // Get the salesOrderHeader
                adapter.SelectCommand = new SqlCommand(
                    "SELECT * " +
                    "FROM EEK_vw_DISTRIBUTION_SALESORDERHEADERS_ZWALUW " +
                    "WHERE SalesOrderHeaderId = " + key,
                    connection
                    );
                DataSet salesOrderHeaders = new DataSet("SALESORDERHEADERS");
                adapter.Fill(salesOrderHeaders);
                DataRow salesOrderHeader = salesOrderHeaders.Tables[0].Rows[0];

                var salesOrder = new ZwaluwSalesOrder();

                string delZip;
                try
                {
                    delZip = (string)salesOrderHeader["DelZip"];
                }
                catch (Exception)
                {
                    delZip = "Unknown";
                }
                string mainDebtorNumber;
                try
                {
                    mainDebtorNumber = (string)salesOrderHeader["MainDebtorNumber"];
                }
                catch
                {
                    mainDebtorNumber = null;
                }

                var orderHeader = new Data.ZwaluwSalesOrder
                {
                    CustomerId = Convert.ToInt32(salesOrderHeader["CustomerId"]),
                    DebtorName = (string)salesOrderHeader["DebtorName"],
                    DebtorNumber = (string)salesOrderHeader["DebtorNumber"],
                    DelAddress = (string)salesOrderHeader["DelAddress"],
                    DelCity = (string)salesOrderHeader["DelCity"],
                    Carrier = (string)salesOrderHeader["Carrier"],
                    DelCountryCode = (string)salesOrderHeader["DelCountryCode"],
                    DelZip = delZip,
                    Description = (string)salesOrderHeader["Description"],
                    OrderNumber = (string)salesOrderHeader["OrderNumber"],
                    Reference = (string)salesOrderHeader["Reference"],
                    WarehouseId = Convert.ToInt32(salesOrderHeader["WarehouseId"]),
                    DeliveryNote = (string)salesOrderHeader["DeliveryNote"],
                    TestIndicator = Convert.ToBoolean(salesOrderHeader["TestIndicator"]),
                    MainDebtorNumber = mainDebtorNumber,
                };

                // Add the header to the salesorder
                salesOrder.SalesOrderHeaders.Add(orderHeader);

                // Get the distribution salesOrderLines
                adapter.SelectCommand = new SqlCommand(
                    "SELECT * " +
                    "FROM EEK_vw_DISTRIBUTION_SALESORDERLINES_ZWALUW " +
                    "WHERE SalesOrderHeaderId = " + key,
                    connection
                    );
                DataSet salesOrderLines = new DataSet("SALESORDERLINES");
                adapter.Fill(salesOrderLines);

                foreach (DataRow salesOrderLine in salesOrderLines.Tables[0].Rows)
                {
                    // Initialize values
                    float itemVolume;
                    try
                    {
                        itemVolume = (float)Convert.ToDouble(salesOrderLine["ItemVolume"]);
                    }
                    catch (Exception)
                    {
                        itemVolume = 0;
                    }
                    float itemWeight;
                    try
                    {
                        itemWeight = (float)Convert.ToDouble(salesOrderLine["ItemWeight"]);
                    }
                    catch (Exception)
                    {
                        itemWeight = 0;
                    }
                    float itemHeight;
                    try
                    {
                        itemHeight = (float)Convert.ToDouble(salesOrderLine["ItemHeight"]);
                    }
                    catch (Exception)
                    {
                        itemHeight = 0;
                    }
                    float itemLength;
                    try
                    {
                        itemLength = (float)Convert.ToDouble(salesOrderLine["ItemLength"]);
                    }
                    catch (Exception)
                    {
                        itemLength = 0;
                    }
                    float itemWidth;
                    try
                    {
                        itemWidth = (float)Convert.ToDouble(salesOrderLine["ItemWidth"]);
                    }
                    catch (Exception)
                    {
                        itemWidth = 0;
                    }
                    float itemSalesUnitsPerColli;
                    try
                    {
                        itemSalesUnitsPerColli = (float)Convert.ToDouble(salesOrderLine["ItemSalesUnitsPerColli"]);
                    }
                    catch (Exception)
                    {
                        itemSalesUnitsPerColli = 1;
                    }
                    string orderLineDescription;
                    try
                    {
                        orderLineDescription = (string)salesOrderLine["OrderLineDescription"];
                    }
                    catch (Exception)
                    {
                        orderLineDescription = (string)salesOrderLine["ItemMainItemDescription"];
                    }
                    var orderLine = new ZwaluwSalesOrderLine
                    {
                        OrderNumber = (string)salesOrderLine["OrderNumber"],
                        OrderLineDescription = orderLineDescription,
                        MainItemQuantity = Convert.ToInt32(salesOrderLine["MainItemQuantity"]),
                        Quantity = Convert.ToInt32(salesOrderLine["Quantity"]),
                        DeliveryDate = (string)salesOrderLine["DeliveryDate"],
                        OrderLineId = Convert.ToInt32(salesOrderLine["OrderLineId"]),
                        ItemMainItemCode = (string)salesOrderLine["ItemMainItemCode"],
                        ItemMainItemDescription = (string)salesOrderLine["ItemMainItemDescription"],
                        ItemCode = (string)salesOrderLine["ItemCode"],
                        ItemDescription = (string)salesOrderLine["ItemDescription"],
                        ItemSalesUnit = (string)salesOrderLine["ItemSalesUnit"],
                        ItemEANCode = (string)salesOrderLine["ItemEANCode"],
                        ItemBrand = (string)salesOrderLine["ItemBrand"],
                        ItemProductGroup = (string)salesOrderLine["ItemProductGroup"],
                        ItemColliUnit = (string)salesOrderLine["ItemColliUnit"],
                        ItemSalesUnitsPerColli = itemSalesUnitsPerColli,
                        ItemVolume = itemVolume,
                        ItemWeight = itemWeight,
                        ItemHeight = itemHeight,
                        ItemLength = itemLength,
                        ItemWidth = itemWidth,
                    };

                    salesOrder.SalesOrderLines.Add(orderLine);
                }
                */

            
                
            
        }
    }
}
