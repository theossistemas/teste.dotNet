
using Microsoft.EntityFrameworkCore;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.INFRA.Context;

namespace TheosLivraria.INFRA.Repository
{
    public class UserRepository : EntityBaseRepository<UserEntity>, IUserRepository
    {
        private readonly TheosLivrariaDbContext _context;
        public UserRepository(TheosLivrariaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetByEmailAsync(string email)
        {
            return await _context
                .User
                .Include(x => x.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.Active);
        }
    }
}
