using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Theos.Library.Domain.Base;

namespace Theos.Library.Domain.Books
{
    public class Book : BaseRelationShip<BookKey>
    {
        public Book()
        {
            Key = new BookKey();
        }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string Author { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(500)")]
        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string Image { get; set; }
    }
}
