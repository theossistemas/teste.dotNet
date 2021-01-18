using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace theos_books.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Website { get; set; }
        public string Information { get; set; }
    }
}
