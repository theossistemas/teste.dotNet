using Theos.Livraria.Domain.Model;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Theos.Livraria.Domain.Model.Usuario;

namespace Theos.Livraria.Domain.Interface.Services
{
    public interface IUsuarioService
    {
        Task<BaseResponse> Inserir(RequestUsuario model);

        Task<BaseResponse> Atualizar(RequestUsuario model);

        Task<BaseResponse> AutenticarUsuario(RequestLoginUsuario request);

        Task<BaseResponse> ObterPorId(long Id);

        Task<BaseResponse> ObterLista();
    }
}
