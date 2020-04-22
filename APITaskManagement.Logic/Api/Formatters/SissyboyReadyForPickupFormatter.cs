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
    public class SissyboyReadyForPickupFormatter : IContentFormatter
    {
        private readonly SissyboyRepository sissyboyRepository = new SissyboyRepository();

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            var item = sissyboyRepository.GetReadyForPickupById(key);

            if (item != null)
            {
                var readyForPickupDto = new SissyboyReadyForPickupDto()
                {
                    PoNumber = item.PoNumber                };

                return JsonConvert.SerializeObject(readyForPickupDto);
            }
            else
            {
                return null;
            }
        }
    }
}
