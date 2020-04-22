using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Api.Models;
using APITaskManagement.Logic.Api.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class SissyboyTrackAndTraceFormatter : IContentFormatter
    {
        private readonly SissyboyRepository sissyboyRepository = new SissyboyRepository();

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            var item = sissyboyRepository.GetTrackAndTraceById(key);

            if (item != null)
            {
                var trackAndTraceDto = new SissyboyTrackAndTraceDto()
                {
                    PoNumber = item.PoNumber,
                    TrackingUrl = item.TrackingUrl
                };

                return JsonConvert.SerializeObject(trackAndTraceDto);
            }
            else
            {
                return null;
            }
        }
    }
}
