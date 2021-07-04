using System.ComponentModel.DataAnnotations;

namespace Livraria.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

    }
}
