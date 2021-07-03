using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheoLib.Dominio.Contratos.Repositorio;
using TheoLib.Dominio.Entidade;

namespace TheoLib.Dados.Repositorio
{
    public class UsuarioRepositorio : RepositorioBase, IUsuarioRepositorio
    {
        public UsuarioRepositorio(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<Usuario> Atualizar(Usuario entity)
        {
            using (var conn = OpenConn())
            { 
                await conn.UpdateAsync<Usuario>(entity);
            }

            return entity;
        }

        public async Task<Usuario> Inserir(Usuario entity)
        {
            using (var conn = OpenConn())
            {
                entity.Id =  await conn.InsertAsync<Usuario>(entity);
            }

            return entity;
        }

        public async Task<IEnumerable<Usuario>> ObterLista()
        {
            using (var conn = OpenConn())
            {
                return await conn.GetAllAsync<Usuario>();
            } 
        }

        public async Task<Usuario> ObterPorId(long Id)
        {
            using (var conn = OpenConn())
            {
                return await conn.GetAsync<Usuario>(Id);
            } 
        }

        public async Task<Usuario> ObterUsuario(string email, string senha)
        {
            var query = "select * from Usuario where Email = @email and Senha = @senha";

            using (var conn = OpenConn())
            {
                return await conn.QueryFirstOrDefaultAsync<Usuario>(query, new { email, senha }); 
            }
        }

        public async Task<bool> UsuarioEstaCadastrado(long id)
        {
            var query = "select count(1) from Usuario where Id = @id";

            using (var conn = OpenConn())
            {
                return await conn.ExecuteScalarAsync<bool>(query, new { id });
            }
        }
    }
}
