using Library.CrossCutting.Auth.Models;

namespace Library.CrossCutting.Auth.Interface;

public interface ITokenService
{
    TokenModel ResponseAuth(LoginModel login);
}