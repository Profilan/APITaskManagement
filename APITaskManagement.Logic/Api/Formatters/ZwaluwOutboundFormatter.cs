using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class ZwaluwOutboundFormatter : IContentFormatter
    {
        private readonly ZwaluwOutboundRepository zwaluwOutboundRepository = new ZwaluwOutboundRepository();

        public string GetJsonContent(int key, DateTime deliveryDate)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Formatting = Formatting.None,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                Converters = new List<JsonConverter> { new DecimalConverter() },
                Culture = new System.Globalization.CultureInfo("en-US")
            };

            try
            {
                var item = zwaluwOutboundRepository.GetById(key);

                if (item != null)
                {
                    var zwaluwOutboundHeaderDto = new ZwaluwOutboundHeaderDto
                    {
                        OrderNumber = item.OrderNumber,
                        SalesOrderHeaderId = item.SalesOrderHeaderId,
                        BarCode = item.Barcode,
                        DebtorNumber = item.DebtorNumber,
                        DebtorName = item.DebtorName,
                        DeliveryAddress = item.DeliveryAddress,
                        DeliveryZip = item.DeliveryZip,
                        DeliveryCity = item.DeliveryCity,
                        DeliveryCountryCode = item.DeliveryCountryCode,
                        DeliveryNote = item.DeliveryNote,
                        DeliveryDate = deliveryDate,
                        Carrier = item.Carrier
                    };

                    IList<ZwaluwOutboundLineDto> lines = new List<ZwaluwOutboundLineDto>();
                    foreach (var line in item.Lines)
                    {
                        lines.Add(new ZwaluwOutboundLineDto
                        {
                            OrderLineId = line.OrderLineId,
                            ItemCode = line.ItemCode,
                            Quantity = (decimal)line.Quantity
                        });
                    }
                    zwaluwOutboundHeaderDto.Lines = lines;

                    return JsonConvert.SerializeObject(zwaluwOutboundHeaderDto, settings);
                }
                else
                {
                    return "[Error]:[Item with id " + key + " does not exist]";
                }
            }
            catch (Exception e)
            {
                return "[Error]:[" + e.Message + "]";
            }
        }

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }
    }
}
