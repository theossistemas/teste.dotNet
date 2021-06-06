using LivrariaWeb.Domain.Interface;
using LivrariaWeb.Domain.Model;
using LivrariaWeb.Dto;
using LivrariaWeb.Infra.Configuration;
using LivrariaWeb.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaWeb.Infra
{
    public class TokenRepository : RepositoryBase<Pessoa>, ITokenRepository
    {
        protected readonly DbContextOptions<ContextBdLivraria> _contextBdLivraria;

        public TokenRepository(DbContextOptions<ContextBdLivraria> contextBdLivraria) : base(contextBdLivraria)
        {
            _contextBdLivraria = contextBdLivraria;
        }

        public bool VerificaSeCadastroExiste(string Email, string Senha)
        {
            if (_dbSet.FirstOrDefault() == null)
                return true;
            else
                return _dbSet.Any(x => (x.Email == Email && x.Senha == Senha));
        }

        public async Task<Pessoa> GetCadastro(string email, string senha)
        {
            using (var data = new ContextBdLivraria(_contextBdLivraria))
            {
                return await data.Set<Pessoa>().FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
            }
        }
    }
}
