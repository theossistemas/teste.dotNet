using System.Threading.Tasks;
using TheoLib.Dominio.Modelo;
using TheoLib.Dominio.Modelo.UsuarioModelo;

namespace TheoLib.Dominio.Contratos.Servicos
{
    public interface IUsuarioServico
    {
        Task<RespostaBase> Inserir(RequisicaoUsuario model);

        Task<RespostaBase> Atualizar(RequisicaoUsuario model);

        Task<RespostaBase> AutenticarUsuario(RequisicaoLoginUsuario request);

        Task<RespostaBase> ObterPorId(long Id);

        Task<RespostaBase> ObterLista();
    }
}
