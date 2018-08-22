using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PostNLLabel : ValueObject<PostNLLabel>
    {
        public PostNLCustomer Customer { get; set; }
        public PostNLMessage Message { get; set; }

        public IList<PostNLShipment> Shipments { get; set; }

        public PostNLLabel(PostNLCustomer customer,
            PostNLMessage message,
            IList<PostNLShipment> shipments)
        {
            Customer = customer;
            Message = message;
            Shipments = shipments;
        }

        protected override bool EqualsCore(PostNLLabel other)
        {
            return (Customer == other.Customer);
        }
    }
}
