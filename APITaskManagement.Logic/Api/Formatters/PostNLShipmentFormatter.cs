using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class PostNLShipmentFormatter : IContentFormatter
    {
        private readonly PostNLRepository postNLRepository;

        public PostNLShipmentFormatter()
        {
            postNLRepository = new PostNLRepository();
        }

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            var item = postNLRepository.GetById(key);

            if (item != null)
            {
                var customerAddress = new PostNLCustomerAddress(
                    item.cAddressType,
                    item.cName,
                    item.cAddress,
                    item.cHousenumber,
                    item.cCity,
                    item.cCountry.Trim(),
                    Regex.Replace(item.cZipcode.ToUpper(), @"\s+", ""));

                var customer = new PostNLCustomer(
                    customerAddress,
                    item.CollectionLocation.ToString(),
                    item.CustomerCode,
                    item.CustomerNumber.ToString());

                var contacts = new List<PostNLContact>();
                var contact = new PostNLContact(
                    item.ContactType,
                    item.Email,
                    item.TelNr);
                contacts.Add(contact);

                var message = new PostNLMessage(
                    item.MessageID,
                    item.MessageTimeStamp.ToString("dd-MM-yyyy HH:mm:ss"),
                    "GraphicFile|PDF");

                var shipments = new List<PostNLShipment>();

                var lines = postNLRepository.ListLinesById(item.MainBarcode);
                foreach (var line in lines)
                {
                    var addresses = new List<PostNLShipmentAddress>();

                    var address = new PostNLShipmentAddress(
                        line.AddressType,
                        line.Name,
                        line.Street,
                        line.HouseNumber,
                        line.HouseNrExt,
                        line.City,
                        line.CountryCode.Trim(),
                        Regex.Replace(line.Zipcode.ToUpper(), @"\s+", ""));

                    addresses.Add(address);

                    var dimension = new PostNLDimension(line.Weight.ToString(), Convert.ToString(line.Volume));

                    var groups = new List<PostNLGroup>();

                    var group = new PostNLGroup(
                        line.GroupCount.ToString(),
                        line.GroupSequence.ToString(),
                        line.GroupType,
                        line.MainBarcode);

                    groups.Add(group);

                    string deliveryDate = line.DeliveryDate.ToString("dd-MM-yyyy HH:mm:ss");
                    if (deliveryDate == "01-01-0001 00:00:00")
                        deliveryDate = null;
                    var shipment = new PostNLShipment(
                        addresses,
                        line.Barcode,
                        contacts,
                        line.Content,
                        dimension,
                        groups,
                        line.ProductCodeDelivery,
                        line.Reference,
                        item.Remark,
                        deliveryDate);

                    shipments.Add(shipment);
                }

                var label = new PostNLLabel(
                    customer,
                    message,
                    shipments);

                return new JavaScriptSerializer().Serialize(label);
            }

            return null;
        }
    }
}
