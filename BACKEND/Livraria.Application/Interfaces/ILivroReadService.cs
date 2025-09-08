using Livraria.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Service.Interfaces
{
    public interface ILivroReadService
    {
        //fazendo a alteração de acordo com o INTERFACE SEGREGATION PRINCIPLE (ISP)

        Task<LivroDTO?> GetLivroByIdAsync(int id);
        Task<IEnumerable<LivroDTO?>> GetLivrosAsync();
        Task<(IEnumerable<LivroDTO?> Livros, int TotalCount)> GetLivrosPagedAsync(int pageNumber, int pageSize);


    }
}
