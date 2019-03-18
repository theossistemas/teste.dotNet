using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
