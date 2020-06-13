using Microsoft.AspNetCore.Authorization;
using System;

namespace MMM.Library.Infra.CrossCutting.Identity.Authorization.CustomPolices
{
    public class ClaimAuthPolice : IAuthorizationRequirement
    {
        public ClaimAuthPolice(string claimName, string claimValue)
        {
            ClaimName = claimName;
            ClaimValue = claimValue;
        }

        public string ClaimName { get; set; }
        public string ClaimValue { get; set; }
    }
}
