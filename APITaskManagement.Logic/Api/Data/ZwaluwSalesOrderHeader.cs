using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class ZwaluwSalesOrderHeader
    {
        [JsonIgnore]
        public virtual int SalesOrderHeaderId { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual string DebtorName { get; set; }
        public virtual string DebtorNumber { get; set; }
        public virtual string DelAddress { get; set; }
        public virtual string DelCity { get; set; }
        public virtual string Carrier { get; set; }
        public virtual string DelCountryCode { get; set; }
        public virtual string DelZip { get; set; }
        public virtual string Description { get; set; }
        public virtual string OrderNumber { get; set; }
        public virtual string Reference { get; set; }
        public virtual int WarehouseId { get; set; }
        public virtual string DeliveryNote { get; set; }
        public virtual bool TestIndicator { get; set; }
        public virtual string MainDebtorNumber { get; set; }

        public virtual ISet<ZwaluwSalesOrderLine> Lines { get; set; }

        public ZwaluwSalesOrderHeader()
        {
            Lines = new HashSet<ZwaluwSalesOrderLine>();
        }
    }
}
