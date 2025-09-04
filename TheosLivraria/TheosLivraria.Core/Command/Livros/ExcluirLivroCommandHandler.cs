using System.Net;
using TheosLivraria.Core.Util;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.Core.Command.Livros
{
    public class ExcluirLivroCommandHandler(IUnitOfWork unitOfWork)
    {
        public async Task<Retorno<bool>> ExecuteAsync(ExcluirLivroCommand command)
        {

            await unitOfWork.BeginTransaction();
            var excluiu = await unitOfWork.LivroRepository.Delete(command.Id);
            if (!excluiu)
            {
                await unitOfWork.Rollback();
                return Retorno<bool>.Falha("Livro não encontrado.", null, HttpStatusCode.NotFound);
            }
            await unitOfWork.Commit();

            return Retorno<bool>.Ok(true, "Livro excluído com sucesso.");

        }
    }
}
