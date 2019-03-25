using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Theos.Library.Domain.Base;

namespace Theos.Library.Domain.Users
{
    public class User : BaseRelationShip<UserKey>
    {
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Login { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string Password { get; set; }
    }
}
