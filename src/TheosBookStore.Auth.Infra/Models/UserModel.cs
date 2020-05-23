namespace TheosBookStore.Auth.Infra.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public string Roles { get; set; }
    }
}
