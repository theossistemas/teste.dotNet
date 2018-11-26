using LC.Domain;
using LC.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Infrastruture.Repositories.Contracts
{
    public interface IBookRepositoy : IRepositoryGeneric<Book>
    {
        IEnumerable<Book> GetOrderedAscendingByName();

        Boolean CheckSlugCreated(string slug);
    }
}
