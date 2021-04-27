using System;
using System.Collections.Generic;
using Livraria.Domain.Security.Entities;
using Livraria.Domain.Security.Models;

namespace Livraria.Domain.Security.Interfaces
{
    // public interface ISecurityUserService<TEntity> where TEntity : class
    public interface ISecurityUserService : IDisposable
    {
        //  TOutputModel GetUser<TInputModel,TOutputModel>(string login, string password) where TInputModel:class where TOutputModel:class;
         User GetUser(string login, string password);
         User GetLogin(string login);
         User Insert(User obj);
        User Update(User obj);
        void Delete(int id);
        IList<User> Select();
        User Select(int id);
    }
}