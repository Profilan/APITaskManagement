using APITaskManagement.Logic.Common.Interfaces;
using APITaskManagement.Logic.Schedulers.Events;

namespace APITaskManagement.Test
{
    public class LastRunChangedEventHandler : IHandler<LastRunChangedEvent>
    {
        public void Handle(LastRunChangedEvent domainEvent)
        {
            
        }
    }
}
