using MMM.IStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Domain.Core.EvetSourcing
{
    public interface IEventSourcingRepository
    {
        Task StoreEvent<TEvent>(TEvent theEvent) where TEvent : Event;
        Task<StoredEvent> GetEventById(Guid id);
        Task<IEnumerable<StoredEvent>> GetAllEvents();
    }
}
