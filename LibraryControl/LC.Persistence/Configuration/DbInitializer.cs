using LC.Domain;
using LC.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC.Persistence.Configuration
{
    public class DbInitializer
    {
        private DataBaseContext context;

        public DbInitializer(DataBaseContext dataContext)
        {
            context = dataContext;
        }

        public void Initialize()
        {

            context.Database.Migrate();

            if (!context.Set<User>().Any())
            {
                User[] list = AddUsers();
                context.AddRange(list);
            }

            context.SaveChanges();

        }

        private static User[] AddUsers()
        {
            return new User[]
                {
                    new User { Name = "Administrador" , Login = "admin" , AcessKey = "e8d95a51f3af4a3b134bf6bb680a213a" },
                    new User { Name = "Gerente" , Login = "gerente" , AcessKey = "e8d95a51f3af4a3b134bf6bb680a213b" }
                };
        }

    }
}
