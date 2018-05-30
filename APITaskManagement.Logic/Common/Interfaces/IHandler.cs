using NServiceBus;

namespace APITaskManagement.Logic.Common.Interfaces
{
    public interface IHandler<T> : IHandleMessages<T>
    {
        void Handle(T args);
    }
}