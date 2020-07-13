using System;

namespace LibraryStore.Core.Data.Dtos
{
    public class BookDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string UrlImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }
    }
}