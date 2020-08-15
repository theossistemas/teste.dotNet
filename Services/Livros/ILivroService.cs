using Models.DTO;
using Services.Base;

namespace Services.Livros
{
    public interface ILivroService : IService<LivroDTO>
    {
        void VerificarSeLivroNaoExiste(LivroDTO livro);
    }
}
