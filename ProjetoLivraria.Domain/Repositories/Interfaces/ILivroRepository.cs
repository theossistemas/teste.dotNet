﻿using ProjetoLivraria.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoLivraria.Domain.Repositories.Interfaces
{
    public interface ILivroRepository : IRepository<Livro>
    {
        IQueryable<Livro> GetAllOrderByTitle();
    }
}
