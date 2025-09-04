using System.Net;
using TheosLivraria.Core.Util;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.Core.Command.Livros
{
    public class CriarLivroCommandHandler(IUnitOfWork unitOfWork)
    {
        public async Task<Retorno<int>> ExecuteAsync(CriarLivroCommand command)
        {
            try
            {

                var LivroExistente = await unitOfWork.LivroRepository.ExisteLivroComNome(command.Titulo);
                if (LivroExistente)
                    return Retorno<int>.Falha("Já existe um Livro cadastrado com este título.", null, HttpStatusCode.Conflict);

                var livro = new Livro(command.Titulo, command.Autor, command.Isbn, command.DataPublicacao, command.Preco, command.Estoque);

                await unitOfWork.LivroRepository.Cadastrar(livro);
                await unitOfWork.BeginTransaction();
                await unitOfWork.Commit();

                return Retorno<int>.Ok(livro.Id, null, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await unitOfWork.Rollback();
                return Retorno<int>.Falha(ex.Message, ex, HttpStatusCode.BadRequest);

            }

        }
    }
}
