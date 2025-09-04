using System.Net;
using TheosLivraria.Core.Util;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.Core.Command.Livros
{
    public class AlterarLivroCommandHandler(IUnitOfWork unitOfWork)
    {
        public async Task<Retorno<bool>> ExecuteAsync(AlterarLivroCommand command)
        {
            try
            {
                var livro = await unitOfWork.LivroRepository.ObterPorUuId(command.Id);
                if (livro == null)
                    return Retorno<bool>.Falha("Livro não encontrado.", null, HttpStatusCode.NotFound);

                livro.AlterarDados(
                    command.Titulo,
                    command.Autor,
                    command.Isbn,
                    command.DataPublicacao,
                    command.Preco,
                    command.Estoque);

                await unitOfWork.BeginTransaction();
                await unitOfWork.Commit();
                return Retorno<bool>.Ok(true, "Livro alterado com sucesso.");
            }
            catch (Exception ex)
            {
                await unitOfWork.Rollback();
                return Retorno<bool>.Falha("Erro ao alterar Livro.", ex, HttpStatusCode.BadRequest);
            }
        }
    }
}
