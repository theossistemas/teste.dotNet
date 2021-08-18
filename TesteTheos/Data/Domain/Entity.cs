using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteTheos.Data
{
    public abstract class Entity
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
        public Guid CriadoPorId { get; set; }

        public DateTime? DataModificacao { get; set; }

        public Guid? ModificadoPorId { get; set; }


        public Usuario CriadoPor { get; set; }

        public Usuario ModificadoPor { get; set; }
    }
}
