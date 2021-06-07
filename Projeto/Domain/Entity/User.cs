using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class User
    {
        public User(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Username { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Password { get; set; }
    }
}
