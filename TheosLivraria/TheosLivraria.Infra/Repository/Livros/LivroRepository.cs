
using Microsoft.EntityFrameworkCore;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Domain.Interface;
using TheosLivraria.Infra.Configuracao;
using TheosLivraria.Infra.Repository.Base;

namespace TheosLivraria.Infra.Repository.Livros
{
    public class LivroRepository(TheosApplicationDbContext context) : RepositoryBase<int, Livro>(context), ILivroRepository
    {
        public async Task<bool> ExisteLivroComNome(string titulo)
        {
            var livro = await context.Livros.FirstOrDefaultAsync(l => l.Titulo == titulo && l.Ativo);

            return livro != null;
        }
    }
}
