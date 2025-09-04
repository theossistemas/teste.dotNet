using Microsoft.EntityFrameworkCore;
using System;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Infra.Configuracao;

namespace TheosLivraria.Infra.Seed
{
    public static class UsuarioRootSeed
    {
        public static void Excute(TheosApplicationDbContext context)
        {
            context.Database.Migrate();

            var emailRoot = "usuarioroot@theoslivraria.com.br";
            var documentoRoot = "05477732938";

            
            if (!context.Usuarios.Any(u => u.Email == emailRoot))
            {
                var usuarioRoot = new Usuario(
                    "Usuário Root",
                    documentoRoot,
                    emailRoot,
                    "123456", 
                    "44991767245",
                    DateTime.Now,
                    Perfil.Administrador
                );

                context.Usuarios.Add(usuarioRoot);
                context.SaveChanges();
            }
        }
    }
}