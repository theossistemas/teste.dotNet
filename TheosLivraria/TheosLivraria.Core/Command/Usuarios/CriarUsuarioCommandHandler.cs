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
    public class CriarUsuarioCommandHandler(IUnitOfWork unitOfWork)
    {
        public async Task<Retorno<int>> ExecuteAsync(CriarUsuarioCommand command)
        {
            try
            {

                var usuarioExistente = await unitOfWork.UsuarioRepository.ObterPorEmail(command.Email);
                if (usuarioExistente != null)
                    return Retorno<int>.Falha("Já existe um usuário cadastrado com este e-mail.", null, HttpStatusCode.Conflict);

                var usuario = new Usuario(command.Nome, command.Documento, command.Email, command.Senha, command.Telefone, command.DataAniversario, (Perfil)command.Perfil);

                await unitOfWork.UsuarioRepository.Cadastrar(usuario);
                await unitOfWork.BeginTransaction();
                await unitOfWork.Commit();

                return Retorno<int>.Ok(usuario.Id, null, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await unitOfWork.Rollback();
                return Retorno<int>.Falha(ex.Message, ex, HttpStatusCode.BadRequest);

            }

        }
    }
}
