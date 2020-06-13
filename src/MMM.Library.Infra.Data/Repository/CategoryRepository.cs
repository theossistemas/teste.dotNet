using MMM.Library.Domain.Interfaces;
using MMM.Library.Domain.Models;
using MMM.Library.Infra.Data.Context;

namespace MMM.Library.Infra.Data.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(LibraryDbContext context) : base(context)
        { }

    }
}
