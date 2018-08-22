using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PostNLCustomerAddress : ValueObject<PostNLCustomerAddress>
    {
        public string AddressType { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string HouseNr { get; set; }
        public string City { get; set; }
        public string Countrycode { get; set; }
        public string Zipcode { get; set; }

        public PostNLCustomerAddress(string addressType,
            string companyName,
            string street,
            string houseNr,
            string city,
            string countrycode,
            string zipcode)
        {
            AddressType = addressType;
            CompanyName = companyName;
            Street = street;
            HouseNr = houseNr;
            City = city;
            Countrycode = countrycode;
            Zipcode = zipcode;
        }

        protected override bool EqualsCore(PostNLCustomerAddress other)
        {
            return (Zipcode == other.Zipcode && HouseNr == other.HouseNr);
        }
    }
}
