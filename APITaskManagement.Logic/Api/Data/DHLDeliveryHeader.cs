using APITaskManagement.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Data
{
    public class DHLDeliveryHeader
    {
        public virtual int Id { get; set; } // OrderIdId
        public virtual DateTime TransmissionCreationDate { get; set; }
        public virtual int TransmissionControlNumber { get; set; }
        public virtual string SendingPartyID { get; set; }
        public virtual string ReceivingPartyID { get; set; }
        public virtual int MessageCount { get; set; }
        public virtual decimal MessageStructureVersion { get; set; }
        public virtual DateTime MessageCreationDate { get; set; }
        public virtual int MessageControlNumber { get; set; }
        public virtual string OrderIdSystem { get; set; }
        public virtual string OrderNr { get; set; }
        public virtual string SenderType { get; set; }
        public virtual string SenderPartnerIdSystem { get; set; }
        public virtual int SenderPartnerIdId { get; set; }
        public virtual int ReceivingPartnerIdId { get; set; }
        public virtual string ReceiverType { get; set; }
        public virtual string ReceiverName { get; set; }
        public virtual string ReceiverAddressName1 { get; set; }
        public virtual string ReceiverAddressCountryCode { get; set; }
        public virtual string ReceiverAddressPostalCode { get; set; }
        public virtual string ReceiverAddressCity { get; set; }
        public virtual string ReceiverAddressStreet { get; set; }
        public virtual string ReceiverAddressPhoneNumber1 { get; set; }
        public virtual string ReceiverAddressEMail { get; set; }
        public virtual string OrderType { get; set; }
        public virtual string ProductType { get; set; }
        public virtual string FreightTerms { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual int Packages { get; set; }

        public virtual ISet<DHLDeliveryLine> DeliveryLines { get; set; }

   
        public DHLDeliveryHeader()
        {
            DeliveryLines = new HashSet<DHLDeliveryLine>();
        }
    }
}
