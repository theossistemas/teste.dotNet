using Microsoft.Extensions.Configuration;  
using Dapper.Contrib.Extensions;   
using System.Threading.Tasks;
using Dapper; 
using System.Collections.Generic; 
using Theos.Livraria.Domain.Entity;
using Theos.Livraria.Domain.Interface.Repository;

namespace Theos.Livraria.Data.Repository
{
    public class LivroRepository : BaseRepository, ILivroRepository
    {
        public LivroRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Livro> Inserir(Livro entity)
        {
            using (var conn = OpenConn())
            {
                entity.Id =  await conn.InsertAsync<Livro>(entity);
            }

            return entity;
        }

        public async Task<Livro> Atualizar(Livro entity)
        {
            using (var conn = OpenConn())
            { 
                await conn.UpdateAsync<Livro>(entity);
            }

            return entity;
        }
         
        public async Task<Livro> ObterPorId(long id)
        { 
            using (var conn = OpenConn())
            {
                return await conn.GetAsync<Livro>(id);
            } 
        }

        public async Task<bool> LivroCadastrado(long id)
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
    }
}
