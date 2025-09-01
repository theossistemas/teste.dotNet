using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Generators;
using TheosLivraria.Application.UseCases.ViewModels.Request.User.GetLogin;
using TheosLivraria.Application.UseCases.ViewModels.Response.User.Login;
using TheosLivraria.Domain.Interfaces.Jwt;
using TheosLivraria.Domain.Interfaces.Notification;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.Application.UseCases.User.GetLogin
{
    public class LoginUseCase(IUserRepository userRepository, IJwtService jwtService, IDomainNotification domainNotification) : ILoginUseCase
    {
        public async Task<ResponseLoginViewModel?> ExecuteAsync(RequestLoginViewModel request)
        {
            var user = await userRepository.GetByEmailAsync(request.Email);
            
            if (!Validate(user, request))
                return null;

            var token = jwtService.GenerateToken(user!);

            return new ResponseLoginViewModel
            {
                Token = token,
                Username = user!.Name,
                Role = user.Role.Name
            };
        }

       private bool Validate(UserEntity? user, RequestLoginViewModel request)
        {
            if (user == null || !user.Active)
            {
                domainNotification.AddNotification(nameof(Validate), "Usuário ou senha inválidos.");
                return false;
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                domainNotification.AddNotification(nameof(Validate), "Usuário ou senha inválidos.");
                return false;
            }
            return true;
        }
    }
}
