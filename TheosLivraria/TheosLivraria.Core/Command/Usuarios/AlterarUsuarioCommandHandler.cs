using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Core.Util;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.Core.Command.Usuarios
{
    public class AlterarUsuarioCommandHandler(IUnitOfWork unitOfWork)
    {
        public async Task<Retorno<bool>> ExecuteAsync(AlterarUsuarioCommand command)
        {
            try
            {
                var usuario = await unitOfWork.UsuarioRepository.ObterPorUuId(command.Id);
                if (usuario == null)
                    return Retorno<bool>.Falha("Usuário não encontrado.", null, HttpStatusCode.NotFound);

                usuario.AlterarDados(
                    command.Nome,
                    command.Documento,
                    command.Email,
                    command.Telefone,
                    command.DataAniversario
                );

                await unitOfWork.BeginTransaction();
                await unitOfWork.Commit();
                return Retorno<bool>.Ok(true, "Usuário alterado com sucesso.");
            }
            catch (Exception ex)
            {
                await unitOfWork.Rollback();
                return Retorno<bool>.Falha("Erro ao alterar usuário.", ex, HttpStatusCode.BadRequest);
            }
        }
    }
}
