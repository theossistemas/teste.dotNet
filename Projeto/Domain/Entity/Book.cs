using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Genre { get; set; }
    }
}
