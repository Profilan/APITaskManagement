using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class DHLDeliveriesFormatter : IContentFormatter
    {
        private readonly DHLDeliveryRepository deliveryRepository = new DHLDeliveryRepository();

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }

        public string GetXmlContent(int key, IDictionary<string, string> properties)
        {
            var item = deliveryRepository.GetById(key);

            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement xmlNs5 = doc.CreateElement("ns5", "Transmission", "http://www.it4logistics.de/i4ldata/ext");
            // xmlNs5.SetAttribute("xmlns:ns5", "http://www.it4logistics.de/i4ldata/ext");
            doc.AppendChild(xmlNs5);

            xmlNs5.AppendChild(doc.CreateElement("TransmissionCreationDate")).AppendChild(doc.CreateTextNode(item.TransmissionCreationDate.ToString("yyyy-MM-ddTHH:mm:ss")));
            xmlNs5.AppendChild(doc.CreateElement("TransmissionControlNumber")).AppendChild(doc.CreateTextNode(item.TransmissionControlNumber.ToString()));
            xmlNs5.AppendChild(doc.CreateElement("SendingPartyID")).AppendChild(doc.CreateTextNode(item.SendingPartyID));
            xmlNs5.AppendChild(doc.CreateElement("ReceivingPartyID")).AppendChild(doc.CreateTextNode(item.ReceivingPartyID));
            xmlNs5.AppendChild(doc.CreateElement("MessageCount")).AppendChild(doc.CreateTextNode(item.MessageCount.ToString()));

            XmlElement xmlMessages = doc.CreateElement("Messages");
            xmlNs5.AppendChild(xmlMessages);

            xmlMessages.AppendChild(doc.CreateElement("MessageStructureVersion")).AppendChild(doc.CreateTextNode(item.MessageStructureVersion.ToString().Replace(',','.')));
            xmlMessages.AppendChild(doc.CreateElement("MessageCreationDate")).AppendChild(doc.CreateTextNode(item.MessageCreationDate.ToString("yyyy-MM-ddTHH:mm:ss")));
            xmlMessages.AppendChild(doc.CreateElement("MessageControlNumber")).AppendChild(doc.CreateTextNode(item.MessageControlNumber.ToString()));

            XmlElement xmlContent = doc.CreateElement("MessageContent");
            xmlMessages.AppendChild(xmlContent);

            XmlElement xmlOrder = doc.CreateElement("ns5", "Order", "http://www.it4logistics.de/i4ldata/ext");
            xmlContent.AppendChild(xmlOrder);

            XmlElement xmlOrderId = doc.CreateElement("OrderId");
            xmlOrder.AppendChild(xmlOrderId);

            xmlOrderId.AppendChild(doc.CreateElement("System")).AppendChild(doc.CreateTextNode(item.OrderIdSystem));
            xmlOrderId.AppendChild(doc.CreateElement("Id")).AppendChild(doc.CreateTextNode(item.Id.ToString()));

            xmlOrder.AppendChild(doc.CreateElement("OrderNr")).AppendChild(doc.CreateTextNode(item.OrderNr));

            XmlElement xmlSender = doc.CreateElement("Sender", "http://www.w3.org/2001/XMLSchema-instance");
            xmlSender.Prefix = "xsi";
            xmlSender.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "ns5:Supplier");


            XmlElement xmlSenderPartnerId = doc.CreateElement("PartnerId");
            xmlSenderPartnerId.AppendChild(doc.CreateElement("System")).AppendChild(doc.CreateTextNode(item.SenderPartnerIdSystem));
            xmlSenderPartnerId.AppendChild(doc.CreateElement("Id")).AppendChild(doc.CreateTextNode(item.SenderPartnerIdId.ToString()));
            xmlSender.AppendChild(xmlSenderPartnerId);

            xmlOrder.AppendChild(xmlSender);

            XmlElement xmlReceiver = doc.CreateElement("Receiver", "http://www.w3.org/2001/XMLSchema-instance");
            xmlReceiver.Prefix = "xsi";
            xmlReceiver.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "ns5:Customer");

            XmlElement xmlReceiverPartnerId = doc.CreateElement("PartnerId");
            xmlReceiverPartnerId.AppendChild(doc.CreateElement("System")).AppendChild(doc.CreateTextNode(item.SenderPartnerIdSystem));
            xmlReceiverPartnerId.AppendChild(doc.CreateElement("Id")).AppendChild(doc.CreateTextNode(item.SenderPartnerIdId.ToString()));
            xmlReceiver.AppendChild(xmlReceiverPartnerId);

            xmlReceiver.AppendChild(doc.CreateElement("Name")).AppendChild(doc.CreateTextNode(item.ReceiverName));

            XmlElement xmlAddress = doc.CreateElement("Address");
            xmlReceiver.AppendChild(xmlAddress);

            xmlAddress.AppendChild(doc.CreateElement("Name1")).AppendChild(doc.CreateTextNode(item.ReceiverAddressName1));
            xmlAddress.AppendChild(doc.CreateElement("Name2"));
            xmlAddress.AppendChild(doc.CreateElement("CountryCode")).AppendChild(doc.CreateTextNode(item.ReceiverAddressCountryCode));
            xmlAddress.AppendChild(doc.CreateElement("PostalCode")).AppendChild(doc.CreateTextNode(item.ReceiverAddressPostalCode));
            xmlAddress.AppendChild(doc.CreateElement("City")).AppendChild(doc.CreateTextNode(item.ReceiverAddressCity));
            xmlAddress.AppendChild(doc.CreateElement("Street")).AppendChild(doc.CreateTextNode(item.ReceiverAddressCity));
            xmlAddress.AppendChild(doc.CreateElement("PhoneNumber1")).AppendChild(doc.CreateTextNode(item.ReceiverAddressPhoneNumber1));
            xmlAddress.AppendChild(doc.CreateElement("PhoneNumber2"));
            xmlAddress.AppendChild(doc.CreateElement("EMail")).AppendChild(doc.CreateTextNode(item.ReceiverAddressEMail));

            xmlOrder.AppendChild(xmlReceiver);

            xmlOrder.AppendChild(doc.CreateElement("OrderType")).AppendChild(doc.CreateTextNode(item.OrderType));
            xmlOrder.AppendChild(doc.CreateElement("ProductType")).AppendChild(doc.CreateTextNode(item.ProductType)); // TODO: moet FV zijn, staat nu onder FreightTerms
            xmlOrder.AppendChild(doc.CreateElement("FreightTerms")).AppendChild(doc.CreateTextNode(item.FreightTerms));
            xmlOrder.AppendChild(doc.CreateElement("OrderDate")).AppendChild(doc.CreateTextNode(item.OrderDate.ToString("yyyy-MM-dd")));

            XmlElement xmlItems = doc.CreateElement("Items");
            xmlOrder.AppendChild(xmlItems);

            foreach (var deliveryLine in item.DeliveryLines)
            {
                // XmlElement xmlItem = doc.CreateElement("Item");
                // xmlItems.AppendChild(xmlItem);

                XmlElement xmlItemId = doc.CreateElement("ItemId");
                xmlItems.AppendChild(xmlItemId);

                xmlItemId.AppendChild(doc.CreateElement("System")).AppendChild(doc.CreateTextNode("DEE"));
                xmlItemId.AppendChild(doc.CreateElement("Id")).AppendChild(doc.CreateTextNode(deliveryLine.Id.ToString()));

                xmlItems.AppendChild(doc.CreateElement("CatalogNr")).AppendChild(doc.CreateTextNode(deliveryLine.CatalogNr));
                xmlItems.AppendChild(doc.CreateElement("ProductNr")).AppendChild(doc.CreateTextNode(deliveryLine.ProductNr));
                xmlItems.AppendChild(doc.CreateElement("ProductName")).AppendChild(doc.CreateTextNode(deliveryLine.ProductName));
                xmlItems.AppendChild(doc.CreateElement("Quantity")).AppendChild(doc.CreateTextNode(deliveryLine.Quantity.ToString().Replace(',', '.')));

                XmlElement xmlVolume = doc.CreateElement("Volume");
                xmlItems.AppendChild(xmlVolume);

                xmlVolume.AppendChild(doc.CreateElement("Amount")).AppendChild(doc.CreateTextNode(deliveryLine.VolumeAmount.ToString().Replace(',', '.')));
                xmlVolume.AppendChild(doc.CreateElement("Unit")).AppendChild(doc.CreateTextNode(deliveryLine.VolumeUnit));

                XmlElement xmlWeight = doc.CreateElement("Weight");
                xmlItems.AppendChild(xmlWeight);

                xmlWeight.AppendChild(doc.CreateElement("Amount")).AppendChild(doc.CreateTextNode(deliveryLine.WeightAmount.ToString()));
                xmlWeight.AppendChild(doc.CreateElement("Unit")).AppendChild(doc.CreateTextNode(deliveryLine.WeightUnit));

                foreach (var barcode in deliveryLine.Barcodes)
                {
                    xmlItems.AppendChild(doc.CreateElement("Barcodes")).AppendChild(doc.CreateTextNode(barcode.Barcode));
                }
            }

            string xmlString = doc.InnerXml;
            xmlString = xmlString.Replace("xsi:Sender", "Sender");
            xmlString = xmlString.Replace("xsi:Receiver", "Receiver");

            return xmlString;
        }
    }
}
