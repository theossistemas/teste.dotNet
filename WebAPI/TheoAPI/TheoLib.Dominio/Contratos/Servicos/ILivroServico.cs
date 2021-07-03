using System.Threading.Tasks;
using TheoLib.Dominio.Modelo;
using TheoLib.Dominio.Modelo.LivroModelo;

namespace TheoLib.Dominio.Contratos.Servicos
{
    public interface ILivroServico
    {
        Task<RespostaBase> Inserir(RequisicaoLivro model);

        Task<RespostaBase> Atualizar(RequisicaoLivro model); 

        Task<RespostaBase> ObterPorId(long Id);

        Task<RespostaBase> ObterLista();
    }
}
