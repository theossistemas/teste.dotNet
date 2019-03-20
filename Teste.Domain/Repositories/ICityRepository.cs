using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Domain.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        IEnumerable<City> GetCityByStateId(int id);
    }
}
