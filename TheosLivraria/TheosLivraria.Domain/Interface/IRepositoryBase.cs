using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Entidades;

namespace TheosLivraria.Domain.Interface
{
    public interface IRepositoryBase<TId, T> where TId : struct where T : EntidadeBase<TId>
    {
        Task<T> Cadastrar(T entidade);
        Task<T> Alterar(T entidade);
        Task<bool> AlterarStatusRegistro(Guid uuid);
       
        Task<bool> Delete(Guid uuid);
        Task<T?> ObterPorUuId(Guid uuid);
    }
}
