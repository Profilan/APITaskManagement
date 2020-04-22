using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class SissyboyDeliveryDateFormatter : IContentFormatter
    {
        private readonly SissyboyRepository sissyboyRepository = new SissyboyRepository();

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            var item = sissyboyRepository.GetDeliveryDateById(key);

            if (item != null)
            {
                var deliveryDateDto = new SissyboyDeliveryDateDto()
                {
                    PoNumber = item.PoNumber,
                    Date = item.Date.ToString("yyyy-MM-dd")
                };

                return JsonConvert.SerializeObject(deliveryDateDto);
            }
            else
            {
                return null;
            }
        }
    }
}
