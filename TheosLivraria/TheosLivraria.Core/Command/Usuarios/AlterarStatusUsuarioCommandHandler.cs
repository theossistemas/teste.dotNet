using System.Net;
using TheosLivraria.Core.Util;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.Core.Command.Usuarios
{
    public class AlterarStatusUsuarioCommandHandler(IUnitOfWork unitOfWork)
    {

        public async Task<Retorno<bool>> ExecuteAsync(AlterarStatusUsuarioCommand command)
        {

            await unitOfWork.BeginTransaction();
            var excluiu = await unitOfWork.UsuarioRepository.AlterarStatusRegistro(command.Id);
            if (!excluiu)
            {
                await unitOfWork.Rollback();
                return Retorno<bool>.Falha("Usuário não encontrado.", null, HttpStatusCode.NotFound);
            }
            await unitOfWork.Commit();

            return Retorno<bool>.Ok(true, "Usuário atualizado com sucesso.");

        }
    }
}
