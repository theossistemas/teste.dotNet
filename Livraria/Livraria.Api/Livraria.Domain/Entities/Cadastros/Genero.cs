using System.ComponentModel.DataAnnotations;

namespace Livraria.Domain.Entities.Cadastros
{
    public class Genero : BaseEntity
    {
        [Required]
        public string Descricao { get; set; }
    }
}
