namespace APITaskManagement.Logic.Common.Interfaces
{
    public interface IApplicationEvent : IDomainEvent
    {
        string EventType { get; }
    }
}