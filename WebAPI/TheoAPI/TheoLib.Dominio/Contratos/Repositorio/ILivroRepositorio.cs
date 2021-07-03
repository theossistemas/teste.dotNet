using System.Collections.Generic;
using System.Threading.Tasks;
using TheoLib.Dominio.Entidade;

namespace TheoLib.Dominio.Contratos.Repositorio
{
    public interface ILivroRepositorio
    {
        Task<Livro> Inserir(Livro entity);

        Task<Livro> Atualizar(Livro entity); 

        Task<Livro> ObterPorId(long Id);

        Task<IEnumerable<Livro>> ObterLista();

        Task<bool> LivroPossuiCadastro(long id);
    }
}
