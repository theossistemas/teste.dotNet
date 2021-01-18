using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace theos_books.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        public string ISBN { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [Required]
        public Publisher Publisher { get; set; }
        [Required]
        public DateTime DateRelease { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
