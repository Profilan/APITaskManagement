using APITaskManagement.Logic.Api.Interfaces;
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
    public class ZwaluwSalesOrder
    {
        public IList<ZwaluwSalesOrderHeader> SalesOrderHeaders;
        public IList<ZwaluwSalesOrderLine> SalesOrderLines;

        public ZwaluwSalesOrder()
        {
            SalesOrderHeaders = new List<ZwaluwSalesOrderHeader>();
            SalesOrderLines = new List<ZwaluwSalesOrderLine>();
        }
    }

    public class ZwaluwSalesOrderHeader
    {
        public int CustomerId;
        public string DebtorName;
        public string DebtorNumber;
        public string DelAddress;
        public string DelCity;
        public string Carrier;
        public string DelCountryCode;
        public string DelZip;
        public string Description;
        public string OrderNumber;
        public string Reference;
        public int WarehouseId;
        public string DeliveryNote;
        public bool TestIndicator;
        

        public ZwaluwSalesOrderHeader()
        {
            
        }
    }

    public class ZwaluwSalesOrderLine
    {
        public string OrderNumber;
        public string OrderLineDescription;
        public int MainItemQuantity;
        public int Quantity;
        public string DeliveryDate;
        public int OrderLineId;
        public string ItemMainItemCode;
        public string ItemMainItemDescription;
        public string ItemCode;
        public string ItemDescription;
        public string ItemSalesUnit;
        public string ItemEANCode;
        public string ItemBrand;
        public string ItemProductGroup;
        public string ItemColliUnit;
        public float ItemSalesUnitsPerColli;
        public float ItemVolume;
        public float ItemWeight;
        public float ItemHeight;
        public float ItemLength;
        public float ItemWidth;

        public ZwaluwSalesOrderLine()
        {

        }
    }

    public class ZwaluwSalesOrderFormatter : IContentFormatter
    {
        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            string connectionstring;
            if (!properties.TryGetValue("connectionstring", out connectionstring))
            {
                if (properties.TryGetValue("connection_string_name", out connectionstring))
                {
                    connectionstring = ConfigurationManager.ConnectionStrings[properties["connection_string_name"]].ConnectionString;
                }
            }
            else
            {
                connectionstring = properties["connectionstring"];
            }

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

                var orderHeader = new ZwaluwSalesOrderHeader
                {
                    CustomerId = Convert.ToInt32(salesOrderHeader["CustomerId"]),
                    DebtorName = (string)salesOrderHeader["DebtorName"],
                    DebtorNumber = (string)salesOrderHeader["DebtorNumber"],
                    DelAddress = (string)salesOrderHeader["DelAddress"],
                    DelCity = (string)salesOrderHeader["DelCity"],
                    Carrier = (string)salesOrderHeader["Carrier"],
                    DelCountryCode = (string)salesOrderHeader["DelCountryCode"],
                    DelZip = (string)salesOrderHeader["DelZip"],
                    Description = (string)salesOrderHeader["Description"],
                    OrderNumber = (string)salesOrderHeader["OrderNumber"],
                    Reference = (string)salesOrderHeader["Reference"],
                    WarehouseId = Convert.ToInt32(salesOrderHeader["WarehouseId"]),
                    DeliveryNote = (string)salesOrderHeader["DeliveryNote"],
                    TestIndicator = Convert.ToBoolean(salesOrderHeader["TestIndicator"]),
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
                    var orderLine = new ZwaluwSalesOrderLine
                    {
                        OrderNumber = (string)salesOrderLine["OrderNumber"],
                        OrderLineDescription = (string)salesOrderLine["OrderLineDescription"],
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

                return new JavaScriptSerializer().Serialize(salesOrder);
                
            }
        }
    }
}
