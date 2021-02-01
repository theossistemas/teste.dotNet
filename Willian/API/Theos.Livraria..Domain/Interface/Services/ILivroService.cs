using Theos.Livraria.Domain.Model;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Theos.Livraria.Domain.Model.Livro;

namespace Theos.Livraria.Domain.Interface.Services
{
    public interface ILivroService
    {
        Task<BaseResponse> Inserir(RequestLivro model);

        Task<BaseResponse> Atualizar(RequestLivro model); 

        Task<BaseResponse> ObterPorId(long Id);

        Task<BaseResponse> ObterLista();
         
    }
}
