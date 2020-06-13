using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MMM.Library.Domain.Core.Interfaces
{
    public interface IUser
    {
        string Name { get; }      
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();

        Guid GetUserId();
        Task<string> GetUserNameById(Guid id);
    }
}
