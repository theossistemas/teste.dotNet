using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Core.Util;
using TheosLivraria.Domain.Interface;

namespace TheosLivraria.Core.Command.Usuarios
{
    public class ExcluirUsuarioCommandHandler(IUnitOfWork unitOfWork)
    {
        public async Task<Retorno<bool>> ExecuteAsync(ExcluirUsuarioCommand command)
        {
           
            await unitOfWork.BeginTransaction();               
            var excluiu = await unitOfWork.UsuarioRepository.Delete(command.Id);
            if (!excluiu)
            {
               await unitOfWork.Rollback();
               return Retorno<bool>.Falha("Usuário não encontrado.", null, HttpStatusCode.NotFound);
            }
            await unitOfWork.Commit();

            return Retorno<bool>.Ok(true, "Usuário excluído com sucesso.");
          
        }
    }
}
