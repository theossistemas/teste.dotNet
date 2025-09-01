
namespace TheosLivraria.Domain.Models.Entities
{
    public class BookEntity  : BaseEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public  int StatusId { get; set; }
        public virtual StatusEntity Status { get; set; } = null!;
    }
}
