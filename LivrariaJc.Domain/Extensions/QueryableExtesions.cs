using LivrariaJc.Domain.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaJc.Domain.Extensions
{
    public static class QueryableExtesions
    {
        public static async Task<PagedQuery<T>> ToPagedQueryAsync<T>(this IOrderedQueryable<T> query, int page, int pageSize)
                where T : class
        {
            var result = new PagedQuery<T>
            {
                PaginaAtual = page,
                TamanhoPagina = pageSize,
            };

            var skip = (page - 1) * pageSize;
            result.Dados = await query.Skip(skip).Take(pageSize).ToListAsync();
            result.PaginaTotal = result.Dados.Count();

            return result;
        }

    }
}
