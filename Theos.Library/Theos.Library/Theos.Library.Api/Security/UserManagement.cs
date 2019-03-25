using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Theos.Library.Api.Models.Login;
using Theos.Library.CrossCutting;
using Theos.Library.CrossCutting.Exceptions;
using Theos.Library.Domain.Users;

namespace Theos.Library.Api.Security
{
    public static class UserManagement
    {
        private static IList<UserInfo> _users;
        private static Guid _anonymousId = Guid.NewGuid();

        public static UserInfo GetUser(Guid token)
        {
            _users = _users ?? new List<UserInfo>();

            var user = _users.FirstOrDefault(f => f.Token.Equals(token));
            if (user == null || user.LastConnection < DateTime.Now.AddMinutes(-EnvironmentProperties.SessionLifeTime))
                throw new SessionException("Your session has been terminated");

            user.LastConnection = DateTime.Now;
            return user;
        }

        public static LoginResponseModel RegisterUser(User user)
        {
            var token = Register(user.Id, ProfileEnum.User);
            return new LoginResponseModel { Login = user.Login, Token = token };
        }

        public static LoginResponseModel RegisterUser()
        {
            var token = Register(_anonymousId, ProfileEnum.Anonymous);
            return new LoginResponseModel { Login = "Anonymous", Token = token };
        }

        public static Guid Validate(HttpRequest request)
        {
            var header = request.Headers.FirstOrDefault(f => f.Key.ToLower().Equals("authorization"));

            if (!header.Value.ToList().Any())
                throw new TokenException("Authorization is missing");

            var tokenRequest = header.Value.ToArray()[0].Replace("Bearer ", string.Empty);
            var token = Guid.Empty;
            if (string.IsNullOrEmpty(tokenRequest) || !Guid.TryParse(tokenRequest, out token) || token == Guid.Empty)
                throw new TokenException("Authorization invalid");

            return GetUser(token).Id;
        }

        private static Guid Register(Guid id, ProfileEnum profile)
        {
            _users = _users ?? new List<UserInfo>();

            if (!_users.Any(a => a.Id.Equals(id)))
            {
                _users.Add(new UserInfo(id, Guid.NewGuid(), profile));
            }

            var response = _users.FirstOrDefault(f => f.Id.Equals(id));
            response.LastConnection = DateTime.Now;

            return response.Token;
        }
    }
}
