using System.Collections.Generic;
using System.Threading.Tasks;
using TheoLib.Dominio.Entidade;

namespace TheoLib.Dominio.Contratos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> Inserir(Usuario entity);

        Task<Usuario> Atualizar(Usuario entity); 

        Task<Usuario> ObterPorId(long Id);

        Task<IEnumerable<Usuario>> ObterLista();

        Task<bool> UsuarioEstaCadastrado(long id);

        Task<Usuario> ObterUsuario(string email, string senha);
    }
}
