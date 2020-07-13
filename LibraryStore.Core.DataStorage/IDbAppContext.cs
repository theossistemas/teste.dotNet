using LibraryStore.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryStore.Core.DataStorage
{
    public interface IDbAppContext : IDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<User> Users { get; set; }
    }
}