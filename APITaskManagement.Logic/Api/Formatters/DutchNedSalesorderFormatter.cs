using APITaskManagement.Logic.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class DutchNedSalesOrderHeader
    {
        public string orderNr;
        public string orderReference;
        public string orderDescription;
        public string debtorName;
        public string delAddress;
        public string delZipcode;
        public string delCity;
        public string delCountry;
        public string delPhone;
        public string delEmail;
        public string supplierName;
        public string supplierRef;
        public string deliveryInstruction;
        public string customerInstruction;
        public int customerId;
        public float rembours;
        public string combiOrderInfo;
        public IList<DutchNedSalesOrderLine> lines;

        public DutchNedSalesOrderHeader()
        {
            lines = new List<DutchNedSalesOrderLine>();

            orderDescription = "";
        }
    }

    public class DutchNedSalesOrderLine
    {
        public int orderLineId;
        public string orderLineDescription;
        public string mainItemCode;
        public int mainItemQuantity;
        public string itemCode;
        public string itemDescription;
        public int quantity;
        public string eanCode;
        public string salesUnit;
        public string colliUnit;
        public string collectionDate;
        public string collectionLocation;
        public float volume;
        public float weight;
        public float height;
        public float length;
        public float width;

        public DutchNedSalesOrderLine()
        {

        }
    }

    public class DutchNedSalesorderFormatter : IContentFormatter
    {
        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();

                connection.Open();

                 // Get the salesOrderHeader
                adapter.SelectCommand = new SqlCommand(
                    "SELECT * " +
                    "FROM EEK_vw_DISTRIBUTION_SALESORDERHEADERS_DUTCHNED " +
                    "WHERE SalesOrderHeaderId = " + key,
                    connection
                    );
                DataSet salesOrderHeaders = new DataSet("SALESORDERHEADERS");
                adapter.Fill(salesOrderHeaders);
                DataRow salesOrderHeader = salesOrderHeaders.Tables[0].Rows[0];

                var orderHeader = new DutchNedSalesOrderHeader
                {
                    orderNr = (string)salesOrderHeader["orderNr"],
                    orderReference = (string)salesOrderHeader["orderReference"],
                    orderDescription = (string)salesOrderHeader["orderDescription"],
                    debtorName = (string)salesOrderHeader["debtorName"],
                    delAddress = (string)salesOrderHeader["delAddress"],
                    delZipcode = (string)salesOrderHeader["delZipcode"],
                    delCity = (string)salesOrderHeader["delCity"],
                    delCountry = (string)salesOrderHeader["delCountry"],
                    delPhone = (string)salesOrderHeader["delPhone"],
                    delEmail = (string)salesOrderHeader["delEmail"],
                    supplierName = (string)salesOrderHeader["supplierName"],
                    supplierRef = ((string)salesOrderHeader["supplierRef"]),
                    deliveryInstruction = (string)salesOrderHeader["deliveryInstruction"],
                    customerInstruction = (string)salesOrderHeader["customerInstruction"],
                    rembours = (float)Convert.ToDouble(salesOrderHeader["rembours"]),
                    combiOrderInfo = (string)salesOrderHeader["combiOrderInfo"]
                };

                // Get the distribution salesOrderLines
                adapter.SelectCommand = new SqlCommand(
                    "SELECT * " +
                    "FROM EEK_vw_DISTRIBUTION_SALESORDERLINES_DUTCHNED " +
                    "WHERE SalesOrderHeaderId = " + key,
                    connection
                    );
                DataSet salesOrderLines = new DataSet("SALESORDERLINES");
                adapter.Fill(salesOrderLines);

                foreach (DataRow salesOrderLine in salesOrderLines.Tables[0].Rows)
                {
                    // Initialize values
                    float volume;
                    try
                    {
                        volume = (float)Convert.ToDouble(salesOrderLine["volume"]);
                    }
                    catch (Exception)
                    {
                        volume = 0;
                    }
                    float weight;
                    try
                    {
                        weight = (float)Convert.ToDouble(salesOrderLine["weight"]);
                    }
                    catch (Exception)
                    {
                        weight = 0;
                    }
                    float height;
                    try
                    {
                        height = (float)Convert.ToDouble(salesOrderLine["height"]);
                    }
                    catch (Exception)
                    {
                        height = 0;
                    }
                    float length;
                    try
                    {
                        length = (float)Convert.ToDouble(salesOrderLine["length"]);
                    }
                    catch (Exception)
                    {
                        length = 0;
                    }
                    float width;
                    try
                    {
                        width = (float)Convert.ToDouble(salesOrderLine["width"]);
                    }
                    catch (Exception)
                    {
                        width = 0;
                    }
                    var orderLine = new DutchNedSalesOrderLine
                    {
                        orderLineId = Convert.ToInt32(salesOrderLine["orderLineId"]),
                        orderLineDescription = (string)salesOrderLine["orderLineDescription"],
                        mainItemCode = (string)salesOrderLine["mainItemcode"],
                        mainItemQuantity = Convert.ToInt32(salesOrderLine["mainItemQuantity"]),
                        itemCode = (string)salesOrderLine["itemCode"],
                        itemDescription = (string)salesOrderLine["itemDescription"],
                        quantity = Convert.ToInt32(salesOrderLine["quantity"]),
                        eanCode = (string)salesOrderLine["eanCode"],
                        salesUnit = (string)salesOrderLine["salesUnit"],
                        colliUnit = (string)salesOrderLine["colliUnit"],
                        collectionDate = (string)salesOrderLine["collectionDate"],
                        collectionLocation = (string)salesOrderLine["collectionLocation"],
                        volume = volume,
                        weight = weight,
                        height = height,
                        length = length,
                        width = width,
                    };

                    orderHeader.lines.Add(orderLine);
                }

                return new JavaScriptSerializer().Serialize(orderHeader);
            }
        }
    }
}
