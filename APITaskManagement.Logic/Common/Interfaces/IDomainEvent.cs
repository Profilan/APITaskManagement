using System;
using System.Linq;

namespace APITaskManagement.Logic.Common.Interfaces
{
    public interface IDomainEvent
    {
        DateTime DateTimeEventOccurred { get; }
    }
}
