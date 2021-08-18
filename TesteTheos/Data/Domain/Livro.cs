using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteTheos.Data
{
    [Table("Livros")]
    public class Livro : Entity, ISoftDelete
    {
        [Required, MaxLength(250)]
        public string Nome { get; set; }

        [Required, MaxLength(250)]
        public string Autor { get; set; }

        [MaxLength(1500)]
        public string Sinopse { get; set; }

        public bool Excluido { get; set; }

        public DateTime? DataExclusao { get; set; }

        public Guid? ExcluidoPorId { get; set; }

        public Usuario ExcluidoPor { get; set; }
    }
}
