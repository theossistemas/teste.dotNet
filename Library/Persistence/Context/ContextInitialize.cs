using Microsoft.EntityFrameworkCore;
using Persistence.Entity;
using System.Linq;

namespace Persistence.Context
{
    public static class ContextInitialize
    {
        public static void EnsureSeedData(this DataContext context)
        {
            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.Livros.Any())
                {
                    context.Livros.AddRange(
                        new Livro
                        {
                            Descricao = "O poder do habito",
                            Ano = "28/02/2012",
                            Autor = "Charles Duhigg"
                        },
                        new Livro
                        {
                            Descricao = "Mindset",
                            Ano = "05/05/2017",
                            Autor = "Carol Dweck"
                        },
                        new Livro
                        {
                            Descricao = "Game of Thrones",
                            Ano = "01/08/1996",
                            Autor = "George R. R. Martin"
                        });
                    context.SaveChanges();
                }
            }
        }
    }
}
