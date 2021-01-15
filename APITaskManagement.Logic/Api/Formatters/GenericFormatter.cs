using APITaskManagement.Logic.Api.Interfaces;
using APITaskManagement.Logic.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Api.Formatters
{
    public class GenericFormatter : IContentFormatter
    {
        private readonly QueueRepository queueRepository = new QueueRepository();

        public string GetJsonContent(int key, IDictionary<string, string> properties)
        {
            try
            {
                var queueItem = queueRepository.GetById(key);

                return queueItem.Body;
            }
            catch (Exception e) 
            {
                return "[Error]:[" + e.Message + "]";
            }
        }
    }
}
