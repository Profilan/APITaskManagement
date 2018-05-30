using System;
using System.Collections.Generic;
using APITaskManagement.Logic.Common.Interfaces;
using System.Configuration;
using APITaskManagement.Logic.Config;
using APITaskManagement.Logic.Queue.Interfaces;
using APITaskManagement.Logic.Schedulers.Data;

namespace APITaskManagement.Logic.Schedulers.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        public IEnumerable<IQueue> List()
        {
            List<IQueue> items = new List<IQueue>();

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

        public IQueue GetByName(string name)
        {
            var queueConfigSection = ConfigurationManager.GetSection("queueSection") as QueueConfigSection;

            if (queueConfigSection != null)
            {
                foreach (QueueElement queueElement in queueConfigSection.Queues)
                {
                    if (queueElement.Name == name)
                    {
                        Type t = Type.GetType("APITaskManagement.Logic.Queue." + queueElement.Type);
                        return (IQueue)Activator.CreateInstance(t, queueElement.Name);
                    }
                }

            }

            return null;
        }
    }
}
