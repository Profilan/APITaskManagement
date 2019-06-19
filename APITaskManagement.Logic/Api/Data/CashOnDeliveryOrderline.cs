using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class CashOnDeliveryOrderline
    {
        public virtual int Id { get; set; }

        public virtual int OrderlineId { get; set; }
        public virtual int CashOnDelivery { get; set; }
    }
}
