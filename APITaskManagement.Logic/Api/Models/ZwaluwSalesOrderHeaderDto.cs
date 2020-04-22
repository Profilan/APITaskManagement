using APITaskManagement.Logic.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Models
{
    public class ZwaluwSalesOrderHeaderDto
    {
        public IList<ZwaluwSalesOrderDto> SalesOrderHeaders;
        public IList<ZwaluwSalesOrderLineDto> SalesOrderLines;

        public ZwaluwSalesOrderHeaderDto()
        {
            SalesOrderHeaders = new List<ZwaluwSalesOrderDto>();
            SalesOrderLines = new List<ZwaluwSalesOrderLineDto>();
        }
    }
}
