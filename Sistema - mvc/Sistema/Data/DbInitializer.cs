using Sistema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Data
{
   public class DbInitializer
   {
      public static void Initialize(SistemaContext context)
      {
         context.Database.EnsureCreated();

         //Busca Registro na tabela categoria

         if (context.Categoria.Any())
         {
            return;
         }

         var categorias = new Categoria[]
            {
               new Categoria{ Descricao="Administração"},
               new Categoria{ Descricao="Astronomia"},
               new Categoria{ Descricao="Cinema"},
               new Categoria{ Descricao="Tecnologia"},
            };

         foreach (Categoria c in categorias)
         {
            context.Categoria.Add(c);
         }
         context.SaveChanges();
      }
   }
}
