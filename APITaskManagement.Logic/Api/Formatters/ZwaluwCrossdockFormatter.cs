using APITaskManagement.Logic.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class ZwaluwCrossdockCollection
    {
        public IList<ZwaluwCrossdock> ZwaluwCrossdocks;

        public ZwaluwCrossdockCollection()
        {

        }
    }

    public class ZwaluwCrossdock
    {
        public int CustomerShipmentId { get; set; }
        public string DeliveryDate { get; set; }
        public string Carrier { get; set; }
        public string DebtorNumber { get; set; }
        public string DebtorName { get; set; }
        public int CustomerId { get; set; }
        public int WarehouseId { get; set; }
        public string DelAddress { get; set; }
        public string DelZip { get; set; }
        public string DelCity { get; set; }
        public string DelCountry { get; set; }
        public string DelPhone { get; set; }
        public string DelEmail { get; set; }
        public string SKUType { get; set; }
        public string SKUId { get; set; }
        public bool TestIndicator { get; set; }
        public string Note { get; set; }
    }

    public class ZwaluwCrossdockFormatter : IContentFormatter
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
                    "FROM EEK_vw_DISTRIBUTION_SHIPMENTS_ZWALUW " +
                    "WHERE Id = " + key,
                    connection
                    );
                DataSet shipments = new DataSet("SHIPMENTS");
                adapter.Fill(shipments);
                var crossdocks = new List<ZwaluwCrossdock>();

                foreach (DataRow shipment in shipments.Tables[0].Rows)
                {
                    string note;
                    try
                    {
                        note = (string)shipment["Note"];
                    }
                    catch
                    {
                        note = "";
                    }
                    var deliveryDate = Convert.ToDateTime(shipment["DeliveryDate"]).ToString("yyyy-MM-dd HH:mm:ss");
                    var crossdock = new ZwaluwCrossdock()
                    {
                        CustomerShipmentId = Convert.ToInt32(shipment["CustomerShipmentId"]),
                        DeliveryDate = deliveryDate,
                        Carrier = (string)shipment["Carrier"],
                        DebtorNumber = ((string)shipment["DebtorNumber"]).Trim(),
                        DebtorName = (string)shipment["DebtorName"],
                        CustomerId = Convert.ToInt32(shipment["CustomerId"]),
                        WarehouseId = Convert.ToInt32(shipment["WarehouseId"]),
                        DelAddress = (string)shipment["DelAddress"],
                        DelZip = (string)shipment["DelZip"],
                        DelCity = (string)shipment["DelCity"],
                        DelCountry = (string)shipment["DelCountry"],
                        DelPhone = (string)shipment["DelPhone"],
                        DelEmail = (string)shipment["DelEmail"],
                        SKUType = (string)shipment["SKUType"],
                        SKUId = (string)shipment["SKUId"],
                        TestIndicator = Convert.ToBoolean(shipment["TestIndicator"]),
                        Note = note
                    };

                    // Add the crossdock to the crossdock list
                    crossdocks.Add(crossdock);
                }

                return new JavaScriptSerializer().Serialize(crossdocks);
            }
        }
    }
}
