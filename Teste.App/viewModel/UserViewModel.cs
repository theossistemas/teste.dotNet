using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Teste.App.viewModel
{
    [DataContract]
    public class UserViewModel
    {
        [DataMember(Name = "Id")]
        public int? Id { get; set; }

        [DataMember(Name = "login")]
        public string Login { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}
