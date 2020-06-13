using Microsoft.EntityFrameworkCore;
using MMM.Library.Domain.Models;

namespace MMM.Library.Infra.Data.DataSeeders
{
    public class DummyDataSeeder
    {
        public static ModelBuilder CategoryDataSeeder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category(1001, "Category 01"));
            modelBuilder.Entity<Category>().HasData(new Category(1001, "Category 02"));
            modelBuilder.Entity<Category>().HasData(new Category(1001, "Category 03"));
            modelBuilder.Entity<Category>().HasData(new Category(1001, "Category 04"));

            return modelBuilder;
        }
    }
}
