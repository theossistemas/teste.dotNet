using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dto
{
    public class UserDto
    {
        public bool Admin { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
