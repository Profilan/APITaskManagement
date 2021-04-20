using System;
using System.Xml.Serialization;

namespace APITaskManagement.Logic.Api.Models
{
    public class DHLMessage
    {
        [XmlElement(ElementName = "MessageControlNumber")]
        public string MessageControlNumber { get; set; }

        [XmlElement(ElementName = "MessageCreationDate")]
        public DateTime MessageCreationDate { get; set; }

        [XmlElement(ElementName = "MessageStructureVersion")]
        public string MessageStructureVersion { get; set; }
    }
}
