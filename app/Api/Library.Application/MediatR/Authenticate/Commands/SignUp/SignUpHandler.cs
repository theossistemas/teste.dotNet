using Library.CrossCutting.Auth.Interface;
using Library.CrossCutting.Auth.Models;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Messages;
using MediatR;
using Serilog;

namespace Library.Application.MediatR.Authenticate.Commands.SignUp;

public class SignUpHandler(IRepository<Login> loginRepository, ITokenService tokenService) : IRequestHandler<SignUpCommand, SignUpResult>
{
    private readonly IRepository<Login> _loginRepository = loginRepository;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<SignUpResult> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        string user = command.UserName.ToLower();
        var findUser = await _loginRepository.FindOne(x => x.UserName.ToLower() == user, cancellationToken);
        if(findUser != null)
            throw new ArgumentException(string.Format(EntityMessages.HAS_VALUE, nameof(Login), $"usuário ({command.UserName})"));

        string email = command.Email.ToLower();
        var findEmail = await _loginRepository.FindOne(x => x.Email.ToLower() == email, cancellationToken);
        if (findEmail != null)
            throw new ArgumentException(string.Format(EntityMessages.HAS_VALUE, nameof(Login), $"Email ({command.Email})"));

        var login = await Login.Insert(command, _loginRepository, cancellationToken);
        var loginModel = new LoginModel
        {
            UserName = login.UserName,
            Email = login.Email,
            Role = login.Role.ToString()
        };
        string token = _tokenService.ResponseAuth(loginModel).Token;

        return new(token);
    }
}