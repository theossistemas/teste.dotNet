using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryStore.Core.Data.Entities
{
    public abstract class BaseEntity : IEntity
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; } = true;
    }
}