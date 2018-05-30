using System.Collections.Generic;
using APITaskManagement.Logic.Schedulers;
using APITaskManagement.Logic.Queue.Repositories;
using System.Configuration;
using System;

namespace APITaskManagement.Logic.Queue
{
    public class QueueDutchNedSalesorder : Queue
    {
        private readonly QueueTableItemRepository _queueTableItemRepository;

        public QueueDutchNedSalesorder(string name) : base(name)
        {
            _queueTableItemRepository = new QueueTableItemRepository();
        }

        public QueueDutchNedSalesorder() : base()
        {
            _queueTableItemRepository = new QueueTableItemRepository();
        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            var requests = new List<Request>();
            var items = _queueTableItemRepository.ListByTask(taskId, 100);

            var formatter = new DutchNedSalesorderFormatter();

            foreach (var item in items)
            {
                var content = formatter.GetJsonContent(item.Key, Properties);

                var request = new Request(item.Id, (int)item.Key, content);

                requests.Add(request);
            }

            return requests;
        }
    }
}
