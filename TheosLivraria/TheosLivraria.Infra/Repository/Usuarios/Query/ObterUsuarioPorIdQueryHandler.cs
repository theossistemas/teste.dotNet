using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Core.Queries.Ususarios;
using TheosLivraria.Core.Util;
using TheosLivraria.Infra.Configuracao;

namespace TheosLivraria.Infra.Repository.Usuarios.Query
{
    public class ObterUsuarioPorIdQueryHandler(TheosApplicationDbContext context)
    {
        public async Task<Retorno<UsuarioDTO>> ExecuteAsync(ObterUsuarioPorIdQuery query)
        {
            var result = await context.Usuarios.FirstAsync(x => x.Uuid == query.Id);
            var ususarioDTO =  new UsuarioDTO(result.Uuid, result.Nome, result.Documento, result.Email, result.Telefone, result.DataNascimento, (int)result.Perfil);

            return Retorno<UsuarioDTO>.Ok(ususarioDTO);
        }
    }
}
