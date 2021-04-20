using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class ZwaluwInboundFormatter : IContentFormatter
    {
        private readonly ZwaluwInboundRepository zwaluwInboundRepository = new ZwaluwInboundRepository();

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }

        public string GetJsonContent(Guid id)
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
                var item = zwaluwInboundRepository.GetById(id);

                if (item != null)
                {
                    var zwaluwInboundHeaderDto = new ZwaluwInboundHeaderDto
                    {
                        TruckReference = item.TruckReference,
                        DeliveryDate = item.DeliveryDate.DateTime,
                        Supplier = item.Supplier
                    };

                    IList<ZwaluwInboundLineDto> lines = new List<ZwaluwInboundLineDto>();
                    foreach (var line in item.Lines)
                    {
                        lines.Add(new ZwaluwInboundLineDto
                        {
                            OrderLineId = line.CustomerLineId,
                            ItemCode = line.ItemCode,
                            PurchaseOrderNumber = line.PurchaseOrderNumber,
                            Quantity = line.Quantity
                        });
                    }
                    zwaluwInboundHeaderDto.Lines = lines;

                    return JsonConvert.SerializeObject(zwaluwInboundHeaderDto, settings);
                }
                else
                {
                    return "[Error]:[Item with id " + id + " does not exist]";
                }
            }
            catch (Exception e)
            {
                return "[Error]:[" + e.Message + "]";
            }
        }
    }
}
