using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Theos.Library.Api.Models.Login
{
    public class LoginResponseModel
    {
        public string Login { get; set; }
        public Guid Token { get; set; }
    }
}
