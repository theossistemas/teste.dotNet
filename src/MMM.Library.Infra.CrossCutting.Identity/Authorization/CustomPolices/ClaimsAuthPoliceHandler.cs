using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MMM.Library.Infra.CrossCutting.Identity.Authorization.CustomPolices
{
    public class ClaimsAuthPoliceHandler : AuthorizationHandler<ClaimAuthPolice>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       ClaimAuthPolice claimAuthPolice)
        {

            var claim = context.User.Claims.FirstOrDefault(c => c.Type == claimAuthPolice.ClaimName);
            if (claim != null && claim.Value.Contains(claimAuthPolice.ClaimValue))
            {
                context.Succeed(claimAuthPolice);
            }

            return Task.CompletedTask;
        }
    }
}
