using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheoLib.Dominio.Contratos.Repositorio;
using TheoLib.Dominio.Entidade;

namespace TheoLib.Dados.Repositorio
{
    public class LivroRepositorio : RepositorioBase, ILivroRepositorio
    {
        public LivroRepositorio(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<Livro> Atualizar(Livro entity)
        {
            using (var conn = OpenConn())
            {
                _ = await conn.UpdateAsync<Livro>(entity);
            }

            return entity;
        }

        public async Task<Livro> Inserir(Livro entity)
        {
            using (var conn = OpenConn())
            {
                entity.Id =  await conn.InsertAsync<Livro>(entity);
            }

            return entity;
        }

        public async Task<bool> LivroPossuiCadastro(long id)
        {
            var sql = "select count(1) from Livro where Id = @id";

            using (var conn = OpenConn())
            {
                return await conn.ExecuteScalarAsync<bool>(sql, new { id });
            }
        }

        public async Task<IEnumerable<Livro>> ObterLista()
        {
            using (var conn = OpenConn())
            {
                return await conn.GetAllAsync<Livro>();
            } 
        }

        public async Task<Livro> ObterPorId(long Id)
        {
            using (var conn = OpenConn())
            {
                return await conn.GetAsync<Livro>(Id);
            } 
        }
    }
}
