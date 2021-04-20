using APITaskManagement.Logic.Api.Data;
using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Schedulers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class FonQOfferFeedFormatterB2B : IContentFormatter
    {
        public string GetJsonContent(FonQOfferFeedB2B item)
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
                var feedDTO = new FonQOfferFeedDTO
                {
                    Schema = "http://incore.azureedge.net/fonQ/supplierarticleapiservice-1.0.json",
                    EAN = item.EAN,
                    MOQ = Convert.ToInt32(item.MOQ),
                    ArticleStatus = item.ArticleStatus,
                    VATPercentage = item.VATPercentage,
                    BrandSupplier = item.BrandSupplier,
                    ArticleDescription = item.ArticleDescription,
                    ArticleNumberSupplier = item.ArticleNumberSupplier,
                    Fragile = item.Fragile,
                    DeliveryLeadTime = item.DeliveryLeadTime,
                    Weight = (decimal)item.Weight,
                    Length = (decimal)item.Length,
                    Width = (decimal)item.Width,
                    Height = (decimal)item.Height,
                    QuantityPerBox = item.QuantityPerBox,
                    QuantityPerOuterCarton = Convert.ToInt32(item.QuantityPerOuterCarton),
                    QuantityFreeOnStock = Convert.ToInt32(item.QuantityFreeOnStock),
                    Prices = JsonConvert.DeserializeObject<IList<FonQOfferFeedArticlePriceDTO>>(item.ArticlePrice)
                };

                if (item.QuantityPerPallet > 0)
                {
                    feedDTO.QuantityPerPallet = Convert.ToInt32(item.QuantityPerPallet);
                }

                if (item.RestockDate != DateTime.MinValue)
                {
                    feedDTO.RestockDate = item.RestockDate.ToString("yyyy-MM-dd");
                }

                return JsonConvert.SerializeObject(feedDTO, settings);
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
