
namespace TheosLivraria.Domain.Models.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Active = true;
        }
        public bool Active { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
