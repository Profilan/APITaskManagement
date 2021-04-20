using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class ZwaluwItemFormatter : IContentFormatter
    {
        private readonly ZwaluwItemRepository zwaluwItemRepository = new ZwaluwItemRepository();

        public string GetJsonContent(int key, IDictionary<string, string> properties)
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
                // Get the item
                var item = zwaluwItemRepository.GetById(key);
                if (item != null)
                {
                    var zwaluwItemDto = new ZwaluwItemDto
                    {
                        ItemCode = item.ItemCode,
                        Description = item.Description,
                        Supplier = item.Supplier,
                        EANCode = !string.IsNullOrEmpty(item.EANCode) ? Convert.ToInt64(item.EANCode) : 0,
                        Length = (decimal)item.Length,
                        Width = (decimal)item.Width,
                        Height = (decimal)item.Height,
                        Volume = (decimal)item.Volume,
                        Weight = (decimal)item.Weight,
                        SalesUnitPerColliUnit = (decimal)item.SalesUnitPerColliUnit
                    };

                    return JsonConvert.SerializeObject(zwaluwItemDto, settings);
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
    }
}
