using Architecture;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account, DbContext>, IAccountRepository
    {
        public AccountRepository(ApplicationDataContext dbContext) : base(dbContext)
        {

        }
    }
}
