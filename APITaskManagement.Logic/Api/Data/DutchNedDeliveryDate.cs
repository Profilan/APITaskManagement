using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class DutchNedDeliveryDate
    {
        public virtual int Id { get; set; }
        public virtual string Carrier { get; set; }
        public virtual DateTime DeliveryDate { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string Note { get; set; }
    }
}
