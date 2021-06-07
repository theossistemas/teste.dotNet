using Data.DatabaseContext;

namespace Data.Repository.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DataContext _dataContext { get; }
        private IBookRepository _bookRepository { get; set; }
        private IUserRepository _userRepository { get; set; }
        public RepositoryWrapper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IBookRepository Book
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(_dataContext);

                return _bookRepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dataContext);

                return _userRepository;
            }
        }

        public void SaveChanges() =>
            _dataContext.SaveChanges();
    }
}
