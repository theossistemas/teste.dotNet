using System;
using System.ComponentModel.DataAnnotations;

namespace TesteTheos.Data
{
    public class Log
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime Data { get; set; } = DateTime.Now;

        [Required, MaxLength(5)]
        public string Level { get; set; }

        [Required, MaxLength(500)]
        public string Mensagem { get; set; }

        public string StackTrace { get; set; }
    }
}
