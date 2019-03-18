using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context,
                                            UserManager<ApplicationUser> userManager,
                                            RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            String adminId1 = "";

            string role1 = "Admin";
            string desc1 = "Grupo de administradores";

            string role2 = "Member";
            string desc2 = "Grupo de membros";

            string password = "Kanium$24";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }

            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            }

            if (await userManager.FindByNameAsync("email1@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "email1@gmail.com",
                    Email = "email1@gmail.com",
                    FirstName = "Admin",
                    LastName = "Super"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminId1 = user.Id;
            }

            if (await userManager.FindByNameAsync("email2@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "email2@gmail.com",
                    Email = "email2@gmail.com",
                    FirstName = "Member",
                    LastName = "Normal"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }

        }
    }
}
