using livraria.Context;
using livraria.Domain.entities;
using livraria.Domain.interfaces;
using livraria.Repository.common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace livraria.Repository
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        private readonly LivrariaContext _context;
        public LivroRepository(LivrariaContext context) : base(context)
        {
            _context = context;
        }

        public override void Create(Livro obj)
        {
            obj.DataCadastro = DateTime.Now;
            base.Create(obj);
        }

        public override void Update(Livro obj, int id)
        {
            obj.DataAlteracao = DateTime.Now;
            base.Update(obj, id);
        }

        public override IList<Livro> GetAll()
        {
            return _context.Livro.Include(x => x.Categoria)
                                 .Include(x => x.Autor).ToList();
        }

        public override Livro GetById(int id)
        {
            return _context.Livro.Include(x => x.Categoria)
                                 .Include(x => x.Autor).FirstOrDefault(x=>x.LivroId == id);
        }
    }
}
