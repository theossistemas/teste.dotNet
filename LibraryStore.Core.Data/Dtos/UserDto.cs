using System;

namespace LibraryStore.Core.Data.Dtos
{
    public class UserDto : BaseDto
    {
        public string Fullname { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }
    }
}