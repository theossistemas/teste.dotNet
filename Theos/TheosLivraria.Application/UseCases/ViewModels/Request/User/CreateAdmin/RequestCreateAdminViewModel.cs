
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.Application.UseCases.ViewModels.Request.User.CreateAdmin
{
    public class RequestCreateAdminViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserEntity ViewModelToModel(RequestCreateAdminViewModel request)
        {
            return new UserEntity
            {
                Name = request.Username,
                Email = request.Email,
            };
        }
    }
}
