using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace TesteTheos.API
{
    public static class IIdentityExtensions
    {
        public static Guid GetId(this IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity)identity;
            var userId = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return Guid.Parse(userId.Value);
        }
    }
}
