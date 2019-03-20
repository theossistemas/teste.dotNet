using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Domain.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        Address GetAddressByPersonId(int personId);
    }
}
