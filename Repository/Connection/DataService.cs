using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Connection
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext _context;

        public DataService(ApplicationContext context)
        {
            this._context = context;
        }

        public void InicializaBD()
        {
            _context.Database.EnsureCreated();
        }
    }
}
