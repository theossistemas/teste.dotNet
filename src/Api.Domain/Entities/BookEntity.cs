using System.Runtime.ConstrainedExecution;
using System;
namespace Api.Domain.Entities
{
    public class BookEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Year { get; set; }
        public string Edition { get; set; }
        public string Publishing { get; set; }
        public string Language { get; set; }
        public UserEntity User { get; set; }
        public Guid IncludedBy { get; set; }
    }
}
