using Dapper.Contrib.Extensions;

namespace biblioteca.models
{using Dapper.Contrib.Extensions;
    [Table("dbo.SistemUser")]
    public class User
    {
        [ExplicitKey]
        public string id { get; set; }
        public string username { get; set; }
        public string userPassword { get; set; }
        public string userRole { get; set; }
    }
}
