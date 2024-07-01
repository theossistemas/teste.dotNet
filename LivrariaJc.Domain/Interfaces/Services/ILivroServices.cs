using LivrariaJc.Domain.Entidades;
using LivrariaJc.Domain.Imput;
using LivrariaJc.Domain.Input;
using LivrariaJc.Domain.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaJc.Domain.Interfaces.Services
{
    public interface ILivroServices
    {
        Task<ServiceResult> ObterTodosAsync(LivroFilterInput input);
        Task<ServiceResult> ObterAsync(int id);
        Task<ServiceResult> NovoAsync(LivroPostDto dto);
        Task<ServiceResult> AlterarAsync(LivroPutDto dto);
        Task<ServiceResult> ExcluirAsync(int id);
    }
}
