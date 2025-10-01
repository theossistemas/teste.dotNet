using MediatR;
using Library.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Library.Application.MediatR.Authenticate.Commands.SignIn;
using Library.Application.MediatR.Authenticate.Commands.SignUp;
using SignInResult = Library.Application.MediatR.Authenticate.Commands.SignIn.SignInResult;

namespace Library.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticateController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Realiza o login no sistema, para acessar os endpoints com nível de acesso administrador.
    /// </summary>
    /// <see cref="SignInCommand.UserName"/>
    /// Parâmetros para autenticar o usuário por nome uo email cadastrado
    /// <see cref="SignInCommand.Password"/>
    /// Senha do usuário cadastrado
    /// <returns>
    /// Retorna um <see cref="OkObjectResult"/> contendo um objeto do tipo 
    /// <see cref="SignInResult"/> Irá retornar o token caso o userName/Login 
    /// e password sejam válidos.
    /// </returns>
    [HttpPost("SignIn")]
    [ProducesResponseType(typeof(SignInResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand query)
        => Ok(await _mediator.Send(query));

    /// <summary>
    /// Realiza o cadastro para o usuário Administrador do sistema
    /// </summary>
    /// <see cref="SignUpCommand.UserName"/>
    /// Parâmetros que representa o Nome do usuário no sistema.
    /// <see cref="SignUpCommand.Email"/>
    /// Parâmetros que representa o Email do usuário no sistema
    /// <see cref="SignUpCommand.Password"/>
    /// Senha do usuário que será cadastrado.
    /// <returns>
    /// Retorna um <see cref="OkObjectResult"/> contendo um objeto do tipo 
    /// <see cref="SignUpResult"/> Irá retornar o token caso registre
    /// o usuário com sucesso.
    /// </returns>
    [HttpPost("SignUp")]
    [ProducesResponseType(typeof(SignUpResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        => CreatedAtAction(nameof(SignUp), await _mediator.Send(command));
}