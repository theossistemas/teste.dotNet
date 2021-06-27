using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Domain.Dto;
using Api.Domain.Entities;
using Api.Domain.Interfaces.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services.User
{
    public class LoginService : ILoginService
    {

        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        public IConfiguration _configuration { get; }
        private IUserRepository _repository;

        public LoginService(SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations, IConfiguration configuration, IUserRepository repository)
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _configuration = configuration;
            _repository = repository;
        }
        public async Task<object> FindByLogin(LoginDto user)
        {
            var baseUser = new UserEntity();

            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByLogin(user.Email);

                var basePassword = await _repository.FindByPassword(user.Password);

                if (baseUser == null && basePassword)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Failed to authenticate"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new ClaimsIdentity(baseUser.Email),
                        new[]
                            {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                            }
                    );
                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);
                    var handler = new JwtSecurityTokenHandler();

                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    return SuccessObject(createDate, expirationDate, token, user);
                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Failed to authenticate"
                };
            }
        }

        private String CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirarionDate,
         JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirarionDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDto user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expexpiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessTokem = token,
                userName = user.Email,
                message = "User successfully logged in"
            };
        }
    }
}
