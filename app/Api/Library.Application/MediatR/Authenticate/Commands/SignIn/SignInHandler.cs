using MediatR;
using Library.Domain.Entities;
using Library.Domain.Messages;
using Library.Domain.Interfaces;
using Library.Shared.Extensions;
using Library.CrossCutting.Auth.Interface;
using Library.CrossCutting.Auth.Models;

namespace Library.Application.MediatR.Authenticate.Commands.SignIn;

public class SignInHandler(IRepository<Login> loginRepository, ITokenService tokenService) : IRequestHandler<SignInCommand, SignInResult>
{
    private readonly IRepository<Login> _loginRepository = loginRepository;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<SignInResult> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        string user = command.UserName.ToLower();
        var login = await _loginRepository.FindOne(x => x.UserName.ToLower() == user || x.Email == user, cancellationToken) ??
            throw new ArgumentException(string.Format(EntityMessages.NOT_FOUND, nameof(Login), $"usuário ({command.UserName})"));

        bool validPassword = EncrypterExtensions.IsValidPassword(command.Password, login.PasswordHash);
        if (validPassword)
        {
            var loginModel = new LoginModel
            {
                UserName = login.UserName,
                Email = login.Email,
                Role = login.Role.ToString()
            };
            string token = _tokenService.ResponseAuth(loginModel).Token;
            return new(token);
        }

        throw new UnauthorizedAccessException("Senha inválida");
    }
}