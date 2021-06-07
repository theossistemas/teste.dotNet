namespace Data.Repository.Wrapper
{
    public interface IRepositoryWrapper
    {
        IBookRepository Book { get; }
        IUserRepository User { get; }
        void SaveChanges();
    }
}
