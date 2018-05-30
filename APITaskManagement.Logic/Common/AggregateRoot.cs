using APITaskManagement.Logic.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITaskManagement.Logic.Common
{
    public abstract class AggregateRoot : Entity<Guid>
    {
        private List<IDomainEvent> _domainEvents;
        public virtual List<IDomainEvent> DomainEvents => _domainEvents;

        public AggregateRoot(Guid id) : base(id)
        {
            
        }

        protected virtual void AddDomainEvent(IDomainEvent newEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(newEvent);
        }

        public virtual void ClearEvents()
        {
            if (_domainEvents is null) return;
            _domainEvents.Clear();
        }
    }
}
