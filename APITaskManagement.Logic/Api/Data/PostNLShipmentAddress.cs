using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PostNLShipmentAddress : ValueObject<PostNLShipmentAddress>
    {
        public string AddressType { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string HouseNr { get; set; }
        public string HouseNrExt { get; set; }
        public string City { get; set; }
        public string Countrycode { get; set; }
        public string Zipcode { get; set; }

        public PostNLShipmentAddress(string addressType,
            string name,
            string street,
            string houseNr,
            string houseNrExt,
            string city,
            string countrycode,
            string zipcode)
        {
            AddressType = addressType;
            Name = name;
            Street = street;
            HouseNr = houseNr;
            HouseNrExt = houseNrExt;
            City = city;
            Countrycode = countrycode;
            Zipcode = zipcode;
        }

        protected override bool EqualsCore(PostNLShipmentAddress other)
        {
            return (Zipcode == other.Zipcode && HouseNr == other.HouseNr && HouseNrExt == other.HouseNrExt);
        }
    }
}
