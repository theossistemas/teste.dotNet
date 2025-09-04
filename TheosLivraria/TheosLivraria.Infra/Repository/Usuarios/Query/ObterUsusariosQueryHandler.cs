using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Core.Queries.Ususarios;
using TheosLivraria.Core.Util;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Infra.Configuracao;

namespace TheosLivraria.Infra.Repository.Usuarios.Query
{
    public class ObterUsusariosQueryHandler(TheosApplicationDbContext context)
    {
        public async Task<Retorno<List<UsuarioGridDTO>>> ExecuteAsync(ObterUsusariosQuery obter)
        {
            var query = context.Usuarios.AsQueryable().OrderBy(o => o.Nome); 
            return Retorno<List<UsuarioGridDTO>>.Ok(await query.Select(s => new UsuarioGridDTO(s.Uuid, s.Nome, s.Email, s.Documento, s.Telefone)).ToListAsync());


        }
    }
}
