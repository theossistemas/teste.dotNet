namespace ProjetoLivraria.Domain.Entities
{
    public class Usuario : Entity
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
