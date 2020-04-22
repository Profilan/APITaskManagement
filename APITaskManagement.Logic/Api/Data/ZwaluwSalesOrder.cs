using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
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
}
