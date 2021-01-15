using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APITaskManagement.Logic.Api.Models
{
    [XmlRootAttribute("TransmissionAcknowledgement", Namespace = "http://www.it4logistics.de/i4ldata/ext")]
    public class DHLTransmissionAcknowledgement
    {
        public string TransmissionControlNumber { get; set; }
        public string SendingPartyID { get; set; }
        public string ReceivingPartyID { get; set; }
        public DHLAcknowledgementDetails AcknowledgementDetails { get; set; }
    }
}
