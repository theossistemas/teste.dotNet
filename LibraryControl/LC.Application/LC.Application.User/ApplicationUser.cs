using LC.Application.User.Configuration;
using LC.Application.User.DataTransferObject;
using LC.Infrastruture.Repositories.Contracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace LC.Application.User
{
    public class ApplicationUser
    {

        readonly IUserRepository _userRepository;
        readonly SigningConfigurations _signingConfigurations;
        readonly TokenConfigurations _tokenConfigurations;

        public ApplicationUser(IUserRepository userRepository, 
            SigningConfigurations signingConfigurations ,
            TokenConfigurations tokenConfigurations)
        {
            _userRepository = userRepository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }


        public IEnumerable<Domain.User> GetAll()
        {
            return _userRepository.GetAsync().Result;
        }

        public TokenDTO Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                Domain.User login = _userRepository.GetUserLogin(userLoginDTO.Login, userLoginDTO.Password);

                if (login == null)
                {
                    throw new Exception("Usuário e/ou senha incorretos!");
                }

                DateTime createdAt = DateTime.Now;
                DateTime expirationDate = createdAt + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);
                string token = GeneratedTokenAuth(login, createdAt, expirationDate);

                return new TokenDTO
                {
                    Authenticated = true,
                    CreatedAt = createdAt,
                    ExpirationDate = expirationDate,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GeneratedTokenAuth(Domain.User login , DateTime createdAt , DateTime expirationDate)
        {
            try
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(login.AcessKey, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, login.AcessKey)
                    }
                );

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfigurations.Issuer,
                    Audience = _tokenConfigurations.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = createdAt,
                    Expires = expirationDate
                });
                return handler.WriteToken(securityToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}