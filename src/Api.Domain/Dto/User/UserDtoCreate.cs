using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dto.User
{
    public class UserDtoCreate
    {
        [Required(ErrorMessage = "Name is Required Field")]
        [StringLength(60, ErrorMessage = "Name must have a maximum of {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required field")]
        [EmailAddress(ErrorMessage = "Email in invalid format")]
        [StringLength(100, ErrorMessage = "Email must have a maximum of {1} characters")]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
    }
}
