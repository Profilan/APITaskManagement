using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITaskManagement.Logic.Queue.Repositories;
using APITaskManagement.Logic.Schedulers;

namespace APITaskManagement.Logic.Queue
{
    public class QueueHelloDialogEmail : Queue
    {
        private readonly QueueTableItemRepository _queueTableItemRepository;

        public QueueHelloDialogEmail(string name) : base(name)
        {
            _queueTableItemRepository = new QueueTableItemRepository();
        }

        public QueueHelloDialogEmail() : base()
        {
            _queueTableItemRepository = new QueueTableItemRepository();
        }

        protected override IList<Request> GetRequestsForTask(Guid taskId)
        {
            var requests = new List<Request>();
            var items = _queueTableItemRepository.ListByTask(taskId, 100);

            var formatter = new HelloDialogEmailFormatter();

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
