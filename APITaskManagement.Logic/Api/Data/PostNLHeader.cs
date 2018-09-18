using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PostNLHeader : Entity<int>
    {
        public virtual string cAddressType { get; set; }
        public virtual string cName { get; set; }
        public virtual string cAddress { get; set; }
        public virtual string cHousenumber { get; set; }
        public virtual string cZipcode { get; set; }
        public virtual string cCity { get; set; }
        public virtual string cCountry { get; set; }
        public virtual int CollectionLocation { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual int CustomerNumber { get; set; }
        public virtual string MessageID { get; set; }
        public virtual DateTime MessageTimeStamp { get; set; }
        public virtual string ShipmentType { get; set; }
        public virtual string AddressType { get; set; }
        public virtual string Reference { get; set; }
        public virtual string Remark { get; set; }
        public virtual string ContactType { get; set; }
        public virtual string Email { get; set; }
        public virtual string TelNr { get; set; }
        public virtual string MainBarcode { get; set; }

        public virtual ISet<PostNLLine> Lines { get; set; }

        public PostNLHeader() : base()
        {
            Lines = new HashSet<PostNLLine>();
        }
    }
}
