
namespace TheosLivraria.Domain.Models.Entities
{
    public class RolesEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
