using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class DutchNedInsertedOrderlinestatus
    {
        public virtual int Id { get; set; }
        public virtual int Status { get; set; }
        public virtual string Description { get; set; }
        public virtual int OrderLineId { get; set; }
    }
}
