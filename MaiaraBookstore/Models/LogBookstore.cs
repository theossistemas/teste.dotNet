using System;
using System.ComponentModel.DataAnnotations;

namespace MaiaraBookstore.Models
{
    public class LogBookstore
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataDeRegistro { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
    }
}
