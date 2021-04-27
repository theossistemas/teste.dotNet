namespace Livraria.Domain.Security.Entities
{
    public class User: SecurityBaseEntity
    {   
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}