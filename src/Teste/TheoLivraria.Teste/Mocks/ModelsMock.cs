using TheoLivraria.Dominio.Entidades;

namespace TheoLivraria.Teste.Mocks
{
    public class ModelsMock
    {
        public static Livro LivroMock()
        {
            var livro = new Livro(0,"Livro Teste","Editora Teste");

            return livro;
        }

        public static Livro LivroExistenteMock()
        {
            var livro = new Livro(1, "Livro Teste", "Editora Teste");

            return livro;
        }
    }
}
