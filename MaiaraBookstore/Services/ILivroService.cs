namespace MaiaraBookstore.Services
{
    public interface ILivroService<T, J>
    {
        bool ValidaSeTituloDeLivroEstaCadastrado(string titulo);

        void Delete(T objeto);

        void SalvarLivro(T objeto);

        T FindById(int Id);

        T EditaLivro(T objeto, J objetoDTO);

    }
}
