using Domain;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using BC = BCrypt.Net.BCrypt;
using System.Linq;
using Architecture;

namespace Seed
{
    public static class AccountSeed
    {
        public static void InitializerSeed(ApplicationDataContext context)
        {
            if (!context.Accounts.Any())
            {
                var userApp = new Account
                {
                    Email = "bookstore@book.com",
                    Name = "app",
                    PasswordHash = BC.HashPassword("#b00kSt0r3"),
                    Role = Role.User,
                    Verified = DateTime.Now,
                    RegisterComplete = true,
                };
                context.Accounts.Add(userApp);

                var userAdmin = new Account
                {
                    Email = "admin.bookstore@book.com",
                    Name = "admin",
                    PasswordHash = BC.HashPassword("#b00kSt0r3"),
                    Role = Role.Admin,
                    Verified = DateTime.Now,
                    RegisterComplete = true,
                };
                context.Accounts.Add(userAdmin);
            }
        }
    }
}
