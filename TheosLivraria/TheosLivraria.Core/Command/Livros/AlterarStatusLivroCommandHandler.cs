using System.Net;
using TheosLivraria.Core.Util;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.Core.Command.Livros
{
    public class AlterarStatusLivroCommandHandler(IUnitOfWork unitOfWork)
    {

        public async Task<Retorno<bool>> ExecuteAsync(AlterarStatusLivroCommand command)
        {

            await unitOfWork.BeginTransaction();
            var excluiu = await unitOfWork.LivroRepository.AlterarStatusRegistro(command.Id);
            if (!excluiu)
            {
                await unitOfWork.Rollback();
                return Retorno<bool>.Falha("Livro não encontrado.", null, HttpStatusCode.NotFound);
            }
            await unitOfWork.Commit();

            return Retorno<bool>.Ok(true, "Livro atualizado com sucesso.");

        }
    }
}
