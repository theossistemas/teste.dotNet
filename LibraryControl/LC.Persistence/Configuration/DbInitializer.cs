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
 
            context.SaveChanges();

        }
    }
}
