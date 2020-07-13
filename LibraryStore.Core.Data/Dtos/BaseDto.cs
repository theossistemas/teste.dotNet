using System;

namespace LibraryStore.Core.Data.Dtos
{
    public abstract class BaseDto : IDto
    {
        public Guid Id { get; set; }
    }
}