using System;
using System.Collections.Generic;
using ProjetoLivraria.Application.ViewModels;

namespace ProjetoLivraria.Application.Interfaces
{
    public interface ILivroAppService : IDisposable
    {
        void Register(LivroViewModel livroViewModel);
        IEnumerable<LivroViewModel> GetAll();
        IEnumerable<LivroViewModel> GetAllOrderByTitle();
        LivroViewModel GetById(Guid id);
        void Update(LivroViewModel livroViewModel);
        void Remove(Guid id);
    }
}
