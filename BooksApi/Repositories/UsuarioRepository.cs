using System.Linq;
using System.Threading.Tasks;
using BooksApi.Data;
using BooksApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Repositories
{
    public class UsuarioRepository : IRepositoryUsuario
    {
        public readonly DataContext _Context;
        
        public UsuarioRepository(DataContext context)
        {
            _Context = context;
            _Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T : class
        {
            _Context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _Context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             _Context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _Context.SaveChangesAsync() > 0;
        }
            public async Task<Usuario> GetUsuario(string email, string password)
            {
                var user = _Context.Usuarios.FirstOrDefaultAsync(x => x. Email.ToLower() == email.ToLower()
                                                                      && x.Senha == password);
                return await user;

            }
        public bool UsuarioExist(string email)
        {
            var reg =  _Context.Usuarios.FirstOrDefault(x => x.Email == email);

            return reg != null;
        }
    }
}