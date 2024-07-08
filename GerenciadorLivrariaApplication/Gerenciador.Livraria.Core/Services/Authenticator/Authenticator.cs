using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Livraria.Core.Services.Authenticator
{
    public class Authenticator
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] papeis = { "Administrador", "Publico" };
            IdentityResult resultadoPapeis;

            foreach (var nomeDoPapel in papeis)
            {
                var papelExiste = await roleManager.RoleExistsAsync(nomeDoPapel);
                if (!papelExiste)
                {
                    resultadoPapeis = await roleManager.CreateAsync(new IdentityRole(nomeDoPapel));
                }
            }

            var usuarioAdministrador = new IdentityUser
            {
                UserName = "admin",
                Email = "giuliano619@gmail.com"
            };
            string senhaDoAdministrador = "_Admin@2024@@$$%%";
            var usuario = await userManager.FindByEmailAsync("giuliano619@gmail.com");

            if (usuario == null)
            {
                var criarUsuarioAdministrador = await userManager.CreateAsync(usuarioAdministrador, senhaDoAdministrador);
                if (criarUsuarioAdministrador.Succeeded)
                {
                    await userManager.AddToRoleAsync(usuarioAdministrador, "Administrador");
                }
            }
        }
    }
}
