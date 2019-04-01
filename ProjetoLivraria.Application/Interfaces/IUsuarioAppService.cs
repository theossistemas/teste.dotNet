using ProjetoLivraria.Application.ViewModels;
using System;
using System.Linq;

namespace ProjetoLivraria.Application.Interfaces
{
    public interface IUsuarioAppService : IDisposable
    {
        UsuarioViewModel Add(UsuarioViewModel item);
        UsuarioViewModel GetById(Guid id);
        IQueryable<UsuarioViewModel> GetAll();
        UsuarioViewModel Update(UsuarioViewModel item);
        void Remove(Guid id);
        UsuarioViewModel Authenticate(string login, string password);
    }
}
