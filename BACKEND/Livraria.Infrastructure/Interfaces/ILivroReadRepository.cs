using Livraria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Infrastructure.Interfaces
{
    public interface ILivroReadRepository
    {
        //fazendo a alteração de acordo com o INTERFACE SEGREGATION PRINCIPLE (ISP)
        Task<Livro?> GetByIdAsync(int id);
        Task<IEnumerable<Livro>> GetAllAsync();
        Task<(IEnumerable<Livro> Livros, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
    }
}
