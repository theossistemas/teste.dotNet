using Theos.Livraria.Domain.Entity;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace Theos.Livraria.Domain.Interface.Repository
{
    public interface ILivroRepository
    {
        Task<Livro> Inserir(Livro entity);

        Task<Livro> Atualizar(Livro entity); 

        Task<Livro> ObterPorId(long Id);

        Task<IEnumerable<Livro>> ObterLista();

        Task<bool> LivroCadastrado(long id);
    }
}
