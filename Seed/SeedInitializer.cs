using Architecture;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Seed
{
    public static class SeedInitializer
    {
        public static void Seed(this IApplicationBuilder app)
        {
            var dbContext = app.ApplicationServices.GetRequiredService<ApplicationDataContext>();
            AccountSeed.InitializerSeed(dbContext);
            dbContext.SaveChanges();
        }
    }
}
