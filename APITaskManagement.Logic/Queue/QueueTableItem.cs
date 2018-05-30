using APITaskManagement.Logic.Common;
using APITaskManagement.Logic.Schedulers;

namespace APITaskManagement.Logic.Queue
{
    public class QueueTableItem : Entity<int>
    {
        public virtual int Key { get; set; }
        public virtual int TryCount { get; set; }
        public virtual Task Task { get; set; }

        protected QueueTableItem()
        {

        }

        public QueueTableItem(int key,
            int tryCount,
            Task task) : this()
        {
            Key = key;
            TryCount = tryCount;
            Task = Task;
        }
    }
}
