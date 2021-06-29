using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Livraria.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Title { get; set; }

        [UIHint("Author")]
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }

        [UIHint("Publisher")]
        public Guid PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }

        public string Country { get; set; }
        public string Language { get; set; }
        public int? Year { get; set; }
        public long? ISBN { get; set; }
        public int? Edition { get; set; }
        public int? Pages { get; set; }
        public int Stock { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string BookInfo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
