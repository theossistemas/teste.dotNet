using System;

namespace LibraryStore.Core.Data.Entities
{
    public interface IEntity
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; }
        public bool Active { get; }
    }
}