namespace MaiaraBookstore.Repository
{
    public interface IRepository<T>
    {
        T FindById(int id);

        T FindByTitulo(string titulo);

        void Save(T objeto);

        void Delete(T objeto);

        void UpDate(T objeto);
    }
}
