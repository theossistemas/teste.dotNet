using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace LC.Application.User.DataTransferObject
{

    [DataContract]
    public class UserLoginDTO
    {
        [DataMember( Name = "login")]
        [Required]
        public string Login { get; set; }

        [DataMember(Name = "password")]
        [Required]
        public string Password { get; set; }
    }
}
