
namespace TheosLivraria.Domain.Models.Entities
{
    public class StatusEntity  : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }
    }
}
