using FluentValidation;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.Commom;
using TheosLivraria.Application.UseCases.User.Auth.DTOs;
using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Interfaces.Services;

namespace TheosLivraria.Application.UseCases.User.Auth
{

    public class AuthUserUseCase : IUseCase<AuthUserInput, AuthUserOutput>
    {


        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<AuthUserInput> _validator;


        public AuthUserUseCase(
            IJwtService jwtService,
            IValidator<AuthUserInput> validator,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _jwtService = jwtService;
        }
        public async Task<Result<AuthUserOutput>> ExecuteAsync(AuthUserInput input)
        {
            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
            {
                var errors = validation.Errors
                    .Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage })
                    .ToList();

                return Result<AuthUserOutput>.Fail("Erro de validação na entidade.", errors);
            }

            var user = await _userRepository.GetByEmailAsync(input.Email);
            if (user == null || !user.IsActive)
            {
                return Result<AuthUserOutput>.Fail("Usuário ou senha inválidos.");
            }

            var samePassword = BCrypt.Net.BCrypt.Verify(input.Password, user.Password);
            if (!samePassword)
            {
                return Result<AuthUserOutput>.Fail("Usuário ou senha inválidos.");
            }

            var token = _jwtService.GenerateToken(user.Id, user.Name, user.Role);

            return Result<AuthUserOutput>.Ok(new AuthUserOutput
            {
                Name = user.Name,
                Role = user.Role.ToString(),
                Token = token
            });
        }
    }
}
