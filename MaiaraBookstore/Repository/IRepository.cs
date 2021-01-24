namespace MaiaraBookstore.Repository
{
    public interface IRepository<T>
    {
        T FindById(int id);

        void Save(T objeto);

        void Delete(T objeto);

        void UpDate(T objeto);
    }
}
