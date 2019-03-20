using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teste.Domain.Repositories;
using Teste.Impl.Context;

namespace Teste.Impl.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly DataContext _dbContext;
        public CityRepository(DataContext dbContext)
       : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<City> GetCityByStateId(int id)
        {
            IQueryable<City> entityCity = _dbContext.Set<City>().AsQueryable();

            var query =
                        from x in entityCity
                        where x.StateId == id
                        select new City
                        {
                            Id = x.Id,
                            Name = x.Name,
                            State = x.State,
                            StateId = x.StateId
                        };

            return query.ToList();
        }
    }
}
