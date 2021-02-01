using Theos.Livraria.Domain.Entity;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace Theos.Livraria.Domain.Interface.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Inserir(Usuario entity);

        Task<Usuario> Atualizar(Usuario entity); 

        Task<Usuario> ObterPorId(long Id);

        Task<IEnumerable<Usuario>> ObterLista();

        Task<bool> UsuarioCadastrado(long id);

        Task<Usuario> ObterUsuario(string email, string senha);
    }
}
