using APITaskManagement.Logic.Queue.Interfaces;
using System.Collections.Generic;

namespace APITaskManagement.Logic.Schedulers.Data
{
    public interface IQueueRepository
    {
        IEnumerable<IQueue> List();

        IQueue GetByName(string name);
    }
}
