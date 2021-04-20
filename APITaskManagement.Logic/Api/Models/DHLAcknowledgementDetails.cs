using System.Xml.Serialization;

namespace APITaskManagement.Logic.Api.Models
{
    public class DHLAcknowledgementDetails
    {
        [XmlElement(ElementName = "ErrorCode")]
        public string ErrorCode { get; set; }

        [XmlElement(ElementName = "ErrorResponse")]
        public string ErrorResponse { get; set; }

        [XmlElement(ElementName = "Message")]
        public string Message { get; set; }
    }
}