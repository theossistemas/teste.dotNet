using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;
using TheosLivraria.Infra.Configuracao;
using TheosLivraria.Infra.Repository.Base;

namespace TheosLivraria.Infra.Repository.Usuarios
{
    public class UsuarioRepository(TheosApplicationDbContext context) : RepositoryBase<int, Usuario>(context), IUsuarioRepository
    {
        public async Task<Usuario?> ObterPorEmail(string email)
        {
                return await context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Ativo);
        }
    }
}
