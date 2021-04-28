using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace teste.dotNet.API.Entities {
    public class BookWriter {
        [Key]
        [Column(Order = 1)]
        public int BookId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int WriterId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        [ForeignKey("WriterId")]
        public Writer Writer { get; set; }
    }
}