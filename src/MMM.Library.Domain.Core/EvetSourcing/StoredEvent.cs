using MMM.IStore.Core.Messages;
using System;

namespace MMM.Library.Domain.Core.EvetSourcing
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }
        public StoredEvent()
        { }
        public Guid Id { get; private set; }
        public string Data { get; private set; }
        public string User { get; private set; }
    }
}
