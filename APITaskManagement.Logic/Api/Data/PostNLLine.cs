using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PostNLLine : Entity<int>
    {
        public virtual string AddressType { get; set; }
        public virtual string City { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string HouseNumber { get; set; }
        public virtual string HouseNrExt { get; set; }
        public virtual string Name { get; set; }
        public virtual string Street { get; set; }
        public virtual string Zipcode { get; set; }
        public virtual string Barcode { get; set; }
        public virtual int Weight { get; set; }
        public virtual int GroupCount { get; set; }
        public virtual Int64 GroupSequence { get; set; }
        public virtual string GroupType { get; set; }
        public virtual string MainBarcode { get; set; }
        public virtual string ProductCodeDelivery { get; set; }
        public virtual string Reference { get; set; }
        public virtual string Content { get; set; }
        public virtual string Volume { get; set; }

        public virtual PostNLHeader Header { get; set; }
    }
}
