using LivrariaWeb.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaWeb.Domain.Interface
{
    public interface ITokenRepository : IRepositoryBase<Pessoa>
    {
        bool VerificaSeCadastroExiste(string Email, string Senha);
        public Task<Pessoa> GetCadastro(string email, string senha);
    }
}
