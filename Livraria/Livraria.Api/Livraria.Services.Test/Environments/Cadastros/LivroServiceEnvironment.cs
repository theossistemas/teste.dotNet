using Livraria.Domain.Entities.Cadastros;
using Livraria.Infra.Data.Interfaces.Repositories.Cadastros;

namespace Livraria.Services.Test.Environments.Cadastros
{
    public static class LivroServiceEnvironment
    {
        public static void Configurar(ILivroRepositorio livroRepositorio)
        {
            var livro1 = new Livro
            {
                Autor = "J. R. R. Tolkien.",
                GeneroId = 4,
                Id = 1,
                Titulo = "O Senhor dos Aneis - A sociedade do anel"
            };

            var livro2 = new Livro
            {
                Autor = "J. R. R. Tolkien.",
                GeneroId = 4,
                Id = 2,
                Titulo = "O Senhor dos Aneis - As duas torres"
            };

            var livro3 = new Livro
            {
                Autor = "J. R. R. Tolkien.",
                GeneroId = 4,
                Id = 3,
                Titulo = "O Senhor dos Aneis - O retorno do rei"
            };

            livroRepositorio.Create(livro1).RunSynchronously();
            livroRepositorio.Create(livro2).RunSynchronously();
            livroRepositorio.Create(livro3).RunSynchronously();
        }
    }
}
