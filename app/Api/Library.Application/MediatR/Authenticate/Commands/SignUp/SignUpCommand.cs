using MediatR;
using Library.Domain.Entities;
using Library.Shared.Extensions;

namespace Library.Application.MediatR.Authenticate.Commands.SignUp;

public record SignUpCommand : IRequest<SignUpResult>
{
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

    public static implicit operator Login(SignUpCommand dto)
        => Login.Create(dto.UserName, dto.Email, EncrypterExtensions.HashPassword(dto.Password));
}