using System.Xml.Serialization;

namespace APITaskManagement.Logic.Api.Models
{
    [XmlRootAttribute("TransmissionAcknowledgement", Namespace = "http://www.it4logistics.de/i4ldata/ext")]
    public class DHLTransmissionAcknowledgement
    {
        [XmlElement(ElementName = "TransmissionControlNumber")]
        public string TransmissionControlNumber { get; set; }

        [XmlElement(ElementName = "SendingPartyID")]
        public string SendingPartyID { get; set; }

        [XmlElement(ElementName = "ReceivingPartyID")]
        public string ReceivingPartyID { get; set; }

        [XmlArray(ElementName = "AcknowledgementDetails")]
        public DHLAcknowledgementDetails AcknowledgementDetails { get; set; }
    }
}
