using System;
using System.Collections.Generic;
using System.Configuration;
using APITaskManagement.Logic.Config;
using APITaskManagement.Logic.Api.Interfaces;

namespace APITaskManagement.Logic.Api.Repositories
{
    public class ApiRepository : IApiRepository
    {
        public IEnumerable<IApi> List()
        {
            List<IApi> items = new List<IApi>();

            var queueConfigSection = ConfigurationManager.GetSection("queueSection") as QueueConfigSection;

            if (queueConfigSection != null)
            {
                foreach (QueueElement queueElement in queueConfigSection.Queues)
                {

                    var queue = GetByName(queueElement.Name);
                    items.Add(queue);
                }
            }

            return items;
        }

        public IApi GetByName(string name)
        {
            var queueConfigSection = ConfigurationManager.GetSection("queueSection") as QueueConfigSection;

            if (queueConfigSection != null)
            {
                foreach (QueueElement queueElement in queueConfigSection.Queues)
                {
                    if (queueElement.Name == name)
                    {
                        Type t = Type.GetType("APITaskManagement.Logic.Api." + queueElement.Type);
                        return (IApi)Activator.CreateInstance(t, queueElement.Name);
                    }
                }

            }

            return null;
        }
    }
}
