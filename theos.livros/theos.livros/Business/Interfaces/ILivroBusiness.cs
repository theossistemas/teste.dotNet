using System.Collections.Generic;
using theos.livros.Entitys;

namespace theos.livros.Business.Interfaces
{
    public interface ILivroBusiness
    {
        List<Livro> ListarLivros();
        bool Inserir(Livro livro);
        bool Atualizar(Livro livro);
        bool Remover(int id);
    }
}