using MediatR;

namespace Library.Application.MediatR.Authenticate.Commands.SignIn;

public record SignInCommand : IRequest<SignInResult>
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}