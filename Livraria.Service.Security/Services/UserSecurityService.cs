using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Livraria.Domain.Security.Entities;
using Livraria.Domain.Security.Interfaces;
using Livraria.Domain.Security.Models;
using Livraria.Service.Security.Validators;

namespace Livraria.Service.Security.Services
{
    // public class UserSecurityService<TEntity> :ISecurityUserService<TEntity> where TEntity : class{
    // public class UserSecurityService : SecurityBaseServices<User>, ISecurityUserService
    public class UserSecurityService : ISecurityUserService
    {
        private readonly IUserRepository _userRepository;
        // private readonly ISecurityBaseRepository<User> _baseRepository;
        private readonly IMapper _mapper;

        public UserSecurityService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;            
            _mapper = mapper;
        }

        public User GetUser(string login, string password)
        {
            var entities = _userRepository.GetUser(login, password);
            var outputModels = _mapper.Map<User>(entities);

            return outputModels;
        }
        public User GetLogin(string login)
        {                        
            var entities = _userRepository.GetLogin(login);
            var outputModels = _mapper.Map<User>(entities);
            return outputModels;
        }

        public User Insert(User obj)
        {   
            Validate(obj, Activator.CreateInstance<UserValidator>());
            _userRepository.Insert(obj);
            obj.Password = "";
            return obj;

        }

        public User Update(User obj)
        {
            Validate(obj, Activator.CreateInstance<UserValidator>());
            _userRepository.Update(obj);
            obj.Password = "";
            return obj;
        }

        public void Delete(int id) => _userRepository.Delete(id);
        public IList<User> Select()
        {
            var entities = _userRepository.Select();

            var users = _mapper.Map<List<User>>(entities);            
            return users;
        }
        public User Select(int id)
        {
            var entities = _userRepository.Select(id);

            var user = _mapper.Map<User>(entities);

            return user;
        }
      
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

         private void Validate(User obj, AbstractValidator<User> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }

}