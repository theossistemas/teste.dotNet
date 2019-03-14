using livraria.Context;
using livraria.Domain.entities;
using livraria.Domain.interfaces;
using livraria.Repository.common;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace livraria.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly LivrariaContext _context;
        public UsuarioRepository(LivrariaContext context) : base(context)
        {
            _context = context;
        }

        public Usuario Login(Usuario usuario)
        {
            return _context.Usuario.Include(x => x.Perfil)
                                  .FirstOrDefault(x => x.Email == usuario.Email && x.Senha == usuario.Senha);
        }
    }
}
