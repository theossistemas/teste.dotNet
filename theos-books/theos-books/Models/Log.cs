using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace theos_books.Models
{
    public enum Type
    {
        Log=0,
        Error=1
    }

    public class Log
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime CreateAt { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public Type Type { get; set; }
    }
}
