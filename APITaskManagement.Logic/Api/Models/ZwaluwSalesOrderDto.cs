using APITaskManagement.Logic.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class ZwaluwSalesOrderDto
    {
        public IList<Data.ZwaluwSalesOrder> SalesOrderHeaders;
        public IList<ZwaluwSalesOrderLine> SalesOrderLines;

        public ZwaluwSalesOrderDto()
        {
            SalesOrderHeaders = new List<Data.ZwaluwSalesOrder>();
            SalesOrderLines = new List<ZwaluwSalesOrderLine>();
        }
    }
}
