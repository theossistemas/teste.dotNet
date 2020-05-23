namespace TheosBookStore.Auth.App.Models
{
    public class AuthenticatedUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }
}
