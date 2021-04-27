namespace Livraria.Domain.Security.Models
{
    public class UserModels
    {
        public UserModels(string login, string password, string role, string name){
            Login = login;
            Password = password;
            Name = name;
            Role = role;
        }
        public  UserModels(){

        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
    }
}