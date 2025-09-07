using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Enums;
using TheosLivraria.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        if (!db.Users.Any(u => u.Email == "admin@teste.com"))
        {
            db.Users.Add(new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Email = "admin@teste.com",
                Password = BCrypt.Net.BCrypt.HashPassword("senha123"),
                Role = RolesEnum.Admin
            });
            db.SaveChanges();
        }
    }
}
