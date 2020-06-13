using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MMM.Library.Infra.CrossCutting.Identity.UsersSedeer
{
    public static class DummyUsersSedeer
    {
        public static async Task AddUsersWithRoles(IServiceProvider serviceProvider)
        {
            // Criar Roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Criar Usuário Admin com Role Admin e todas as Claims previamente relacionadas
            IdentityUser user = await UserManager.FindByEmailAsync("admin@gmail.com");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                };
                await UserManager.CreateAsync(user, "Admin@123");

                await UserManager.AddToRoleAsync(user, "Admin");
                await UserManager.AddClaimsAsync(user, SystemAuthClaims.WriteClaimsList());
                await UserManager.AddClaimsAsync(user, SystemAuthClaims.ReadClaimsList());
                

            }           

            // Criar Usuário User com Role User e todas as Claims de Leitura previamente relacionadas
            IdentityUser user1 = await UserManager.FindByEmailAsync("user@gmail.com");

            if (user1 == null)
            {
                user1 = new IdentityUser()
                {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                };
                await UserManager.CreateAsync(user1, "User@123");

                await UserManager.AddToRoleAsync(user1, "User");
                await UserManager.AddClaimsAsync(user1, SystemAuthClaims.ReadClaimsList());

                await UserManager.AddClaimAsync(user1, SystemAuthClaims.IsDeveloperClaim);
            }           
        }
    }
}




