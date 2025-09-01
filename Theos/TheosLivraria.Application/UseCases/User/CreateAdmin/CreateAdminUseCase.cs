
using Microsoft.Extensions.Logging;
using TheosLivraria.Application.UseCases.ViewModels.Request.User.CreateAdmin;
using TheosLivraria.Application.UseCases.ViewModels.Response.User.CreateAdmin;
using TheosLivraria.Domain.Interfaces.Notification;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Interfaces.UnitOfWork;
using TheosLivraria.Domain.Models.Enum;

namespace TheosLivraria.Application.UseCases.User.CreateAdmin
{
    public class CreateAdminUseCase(IUserRepository userRepository,
                                        IUnitOfWork unitOfWork,
                                        IDomainNotification domainNotification,
                                        ILogger<CreateAdminUseCase> logger
                                    ) : ICreateAdminUserCase
    {
        public async Task<ResponseCreateAdminViewModel?> ExecuteAsync(RequestCreateAdminViewModel request)
        {
            if (!await Validate(request)) return null;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = request.ViewModelToModel(request);

            newUser.PasswordHash = passwordHash;
            newUser.RoleId = (int)RolesEnum.Admin;

            await userRepository.AddAsync(newUser);
            await unitOfWork.CommitAsync();

            return new ResponseCreateAdminViewModel { Id = newUser.Id };
        }

        private async Task<bool> Validate(RequestCreateAdminViewModel request)
        {
            var user = await userRepository.GetByEmailAsync(request.Email);
            
            if (user != null)
            {
                domainNotification.AddNotification(nameof(Validate), "Já existe um usuário cadastrado com este e-mail.");
                logger.LogWarning("Tentativa de criar admin duplicado: {Username}", request.Username);
                return false;
            }
           
            var validation = new CreateAdminValidation();

            if (!validation.Validate(request).IsValid)
            {
                foreach (var error in validation.Validate(request).Errors)
                {
                    domainNotification.AddNotification(error.PropertyName, error.ErrorMessage);
                }
                return false;
            }

            return true;
        }
    }
}
