using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PostNLCustomer : ValueObject<PostNLCustomer>
    {
        public PostNLCustomerAddress Address { get; set; }
        public string CollectionLocation { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerNumber { get; set; }

        public PostNLCustomer(PostNLCustomerAddress address,
            string collectionLocation,
            string customerCode,
            string customerNumber)
        {
            Address = address;
            CollectionLocation = collectionLocation;
            CustomerCode = customerCode;
            CustomerNumber = customerNumber;
        }

        protected override bool EqualsCore(PostNLCustomer other)
        {
            return (Address == other.Address);
        }
    }
}
