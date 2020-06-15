using MMM.IStore.Core.Messages;
using System;
using System.Collections.Generic;

namespace MMM.Library.Domain.Core.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        // Events stack
        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        public void AddEvent(Event theEvent)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(theEvent);
        }
        public void RemoveEvent(Event eventItem)
        {
            _notifications?.Remove(eventItem);
        }
        public void ClearEvents()
        {
            _notifications?.Clear();
        }

        public void MarkAsDelete()
        {
            IsDeleted = true;
        }

        // Good practices 
        // Compare entity: must have same type and same Id
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        // Operator equals implementation
        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        // Force unique hashcode using the formula: generated hashcode * 109 + Id (109 is an aleatory number)
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 109) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
