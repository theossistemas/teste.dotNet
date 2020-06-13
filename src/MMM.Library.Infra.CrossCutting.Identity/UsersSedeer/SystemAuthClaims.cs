using System.Collections.Generic;
using System.Security.Claims;

namespace MMM.Library.Infra.CrossCutting.Identity.UsersSedeer
{
    public static class SystemAuthClaims
    {
        public static Claim IsDeveloperClaim = new Claim("IsDeveloper", "true");
        public static List<Claim> WriteClaimsList()
        {
            List<string> listResource = new List<string>();
            listResource.Add("Book");
            listResource.Add("Category");

            List<Claim> claims = new List<Claim>();

            foreach (var resource in listResource)
            {
                claims.Add(new Claim(resource, "Update"));
                claims.Add(new Claim(resource, "Add"));
                claims.Add(new Claim(resource, "Delete"));
            }

            return claims;
        }


        public static List<Claim> ReadClaimsList()
        {
            List<string> listResource = new List<string>();
            listResource.Add("Book");
            listResource.Add("Category");

            List<Claim> claims = new List<Claim>();

            foreach (var resource in listResource)
            {
                claims.Add(new Claim(resource, "Read"));
            }

            return claims;
        }
    }
}
