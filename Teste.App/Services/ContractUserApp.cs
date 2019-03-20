using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Teste.App.viewModel;

namespace Teste.App.Services
{
    public class ContractUserApp
    {
        private IRepository<User> _rep;

        public ContractUserApp(IRepository<User> rep)
        {
            _rep = rep;
        }

        public virtual UserViewModel GetById(int id)
        {
            try
            {
                var entity = _rep.Get(id);

                UserViewModel user = new UserViewModel
                {
                    Id = entity.Id,
                    Login = entity.Login,
                    Password = entity.Password,

                };
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual User GetByLogin(string login)
        {
            return _rep.GetAll(null).Where(x => x.Login == login).FirstOrDefault();
        }

        public virtual User SaveUser(UserViewModel modelView)
        {
            try
            {

                User entity = new User
                {
                    Login = modelView.Login,
                    Password = modelView.Password
                };
                _rep.Insert(entity);

                return entity;
            }
            catch
            {
                throw new ValidationException();
            }

        }

        public virtual User EditUser(UserViewModel view)
        {
            try
            {
                User user = _rep.Get(view.Id.Value);

                if (user != null)
                {
                    user.Login = string.IsNullOrWhiteSpace(view.Login) ? user.Login : view.Login;
                    user.Password = string.IsNullOrWhiteSpace(view.Password) ? user.Password : view.Password;
                    _rep.Update(user);
                }

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUser(int id)
        {
            User User = _rep.Get(id);
            if (User != null)
            {
                _rep.Delete(User);
            }
        }

        public List<User> GetAll(string login)
        {
            return _rep.GetAll(null).Where(x => (string.IsNullOrWhiteSpace(login) || x.Login.ToUpper().Contains(login.ToUpper()))).ToList();
        }
    }
}
