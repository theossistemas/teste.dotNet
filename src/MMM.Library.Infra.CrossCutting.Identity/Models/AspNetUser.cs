using Microsoft.AspNetCore.Http;
using MMM.Library.Domain.Core.Interfaces;
using MMM.Library.Infra.CrossCutting.Identity.Authorization;
using MMM.Library.Infra.CrossCutting.Identity.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MMM.Library.Infra.CrossCutting.Identity.Models
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ApplicationDbContext _dbContext;

        public AspNetUser(IHttpContextAccessor accessor, ApplicationDbContext db)
        {
            _accessor = accessor;
            _dbContext = db;
        }

        public string Name => GetName();

        private string GetName()
        {
            return _accessor.HttpContext.User.Identity.Name ??
                   _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }


        public async Task<string> GetUserNameById(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id.ToString());

            // TODO-P: adjust user properties
            if (user == null)
            {
                return "UserTestMode";
            }

            return user.UserName;
        }

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

    }
   
}
