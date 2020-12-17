using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class DHLDeliveryLineIdentifier : ValueObject<DHLDeliveryLineIdentifier>
    {
        public virtual int OrderIdId { get; set; }
        public virtual int OrsrgId { get; set; }

        protected override bool EqualsCore(DHLDeliveryLineIdentifier other)
        {
            return (OrderIdId == other.OrderIdId && OrsrgId == other.OrsrgId);
        }
    }
}
