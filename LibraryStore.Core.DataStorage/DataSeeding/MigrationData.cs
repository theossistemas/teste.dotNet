using LibraryStore.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryStore.Core.DataStorage.DataSeeding
{
    public static class MigrationData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.NewGuid(), Fullname = "Administrator", Username = "admin", Password = "admin", CreatedAt = new DateTime(2020, 1, 1), Active = true, Role = "manager" }
            );
        }
    }
}