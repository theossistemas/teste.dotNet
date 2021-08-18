using System;
using System.ComponentModel.DataAnnotations;

namespace TesteTheos.API.Models
{
    public class LivroViewModel
    {
        public Guid Id { get; set; }

        [Required, MaxLength(250)]
        public string Nome { get; set; }

        [Required, MaxLength(250)]
        public string Autor { get; set; }
    }
}
