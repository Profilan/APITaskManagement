using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class ZwaluwSalesOrderDto
    {
        public int CustomerId { get; set; }
        public string DebtorName { get; set; }
        public string DebtorNumber { get; set; }
        public string DelAddress { get; set; }
        public string DelCity { get; set; }
        public string Carrier { get; set; }
        public string DelCountryCode { get; set; }
        public string DelZip { get; set; }
        public string Description { get; set; }
        public string OrderNumber { get; set; }
        public string Reference { get; set; }
        public int WarehouseId { get; set; }
        public string DeliveryNote { get; set; }
        public bool TestIndicator { get; set; }
        public string MainDebtorNumber { get; set; }
    }
}
