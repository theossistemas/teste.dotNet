using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AccountUpdateDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
