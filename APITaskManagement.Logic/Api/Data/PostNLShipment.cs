using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class PostNLShipment : ValueObject<PostNLShipment>
    {
        public IList<PostNLShipmentAddress> Addresses { get; set; }
        public string Barcode { get; set; }
        public IList<PostNLContact> Contacts { get; set; }
        public string Content { get; set; }
        public PostNLDimension Dimension { get; set; }
        public IList<PostNLGroup> Groups { get; set; }
        public string ProductCodeDelivery { get; set; }
        public string Reference { get; set; }
        public string Remark { get; set; }

        public PostNLShipment(IList<PostNLShipmentAddress> addresses,
            string barcode,
            IList<PostNLContact> contacts,
            string content,
            PostNLDimension dimension,
            IList<PostNLGroup> groups,
            string productCodeDelivery,
            string reference,
            string remark)
        {
            Addresses = addresses;
            Barcode = barcode;
            Contacts = contacts;
            Content = content;
            Dimension = dimension;
            Groups = groups;
            ProductCodeDelivery = productCodeDelivery;
            Reference = reference;
            Remark = remark;
        }

        protected override bool EqualsCore(PostNLShipment other)
        {
            return (Barcode == other.Barcode && Reference == other.Reference);
        }
    }
}
