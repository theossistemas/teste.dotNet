using Persistence.Entity;
using System.Collections.Generic;

namespace TestLibrary.Mock
{
    public class LivroTestMock
    {
        public static List<Livro> ObterLivroMock()
        {
            return new List<Livro>()
            {
                new Livro()
                {
                    Id = 1,
                    Descricao = "livro 1",
                    Ano = "10/10/2010",
                    Autor = "Desconhecido"
                },
                new Livro()
                {
                    Id = 2,
                    Descricao = "livro 2",
                    Ano = "01/01/2015",
                    Autor = "Autor"
                }
            };
        }
    }
}
