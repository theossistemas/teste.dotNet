using System;
using System.Collections.Generic;

#nullable disable

namespace TheosTestAPI.Entity
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
