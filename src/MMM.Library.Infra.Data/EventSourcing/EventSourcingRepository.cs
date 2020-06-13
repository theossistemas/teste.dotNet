using Microsoft.EntityFrameworkCore;
using MMM.IStore.Core.Messages;
using MMM.Library.Domain.Core.EvetSourcing;
using MMM.Library.Domain.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMM.Library.Infra.Data.EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly EventSourcingDbContext _dbContext;
        private readonly IUser _user;

        public EventSourcingRepository(EventSourcingDbContext context, IUser user)
        {
            _dbContext = context;
            _user = user;
        }

        public async Task StoreEvent<TEvent>(TEvent theEvent) where TEvent : Event
        {
            var userId = _user.GetUserId();

            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                await _user.GetUserNameById(userId));

            _dbContext.StoredEvents.Add(storedEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<StoredEvent> GetEventById(Guid id)
        {
            return await _dbContext.StoredEvents.Where(p => p.Id == id)
                .OrderByDescending(p => p.Timestamp).FirstOrDefaultAsync();
        }
    
        public async Task<IEnumerable<StoredEvent>> GetAllEvents()
        {
            return await _dbContext.StoredEvents.OrderByDescending(p => p.Timestamp).ToListAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
