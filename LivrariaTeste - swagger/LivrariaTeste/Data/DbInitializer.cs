using LivrariaTeste.Data;
using LivrariaTeste.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Data
{
   public class DbInitializer
   {
      public static void Initialize(LivrariaTesteContext context)
      {
         context.Database.EnsureCreated();
      }
   }
}
