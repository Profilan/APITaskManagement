using Microsoft.AspNet.SignalR.Hubs;
using StructureMap;

namespace APITaskManagement.Service.Hubs
{
    public class HubActivator : IHubActivator
    {
        private readonly IContainer _container;

        public HubActivator(IContainer container)
        {
            _container = container;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub)_container.GetInstance(descriptor.HubType);
        }
    }
}
