using JeffersonMello.Livraria.Business.Contract;
using JeffersonMello.Livraria.Common.Helper;
using JeffersonMello.Livraria.Common.Response;
using JeffersonMello.Livraria.Model.Abstract;
using JeffersonMello.Livraria.Model.Filter.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ninject;
using System;
using System.Collections.Generic;

namespace JeffersonMello.Livraria.API.Controllers.Abstract
{
    public abstract class ApiControllerBase<TEntity, TFilter, TBusiness> : ControllerBase
       where TEntity : EntityBase, new()
       where TFilter : FilterBase, new()
       where TBusiness : IBusiness<TEntity, TFilter>
    {
        #region Protected Fields

        protected DbContext dbContext;
        protected TBusiness business;

        #endregion Protected Fields

        #region Public Constructors

        public ApiControllerBase(DbContext dbContext)
        {
            try
            {
                this.dbContext = dbContext;
                business = Program.kernel.Get<TBusiness>();
            }
            catch (Exception)
            {
            }
        }

        public ApiControllerBase()
        {
            try
            {
                business = Program.kernel.Get<TBusiness>();
            }
            catch (Exception)
            {
            }
        }

        #endregion Public Constructors

        #region Public Methods

        [Route("List"), HttpGet, HttpOptions]
        public virtual Response<IList<TEntity>> Get()
        {
            var response = new Response<IList<TEntity>>();

            try
            {
                response.Success = true;
                response.Data = business.Get();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ExceptionHelper.GetMessage(ex);
            }

            return response;
        }

        [Route("Filter"), HttpPost, HttpOptions]
        public virtual Response<IList<TEntity>> Get([FromBody] TFilter filter)
        {
            var response = new Response<IList<TEntity>>();

            try
            {
                response.Success = true;
                response.Data = business.Get(filter);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ExceptionHelper.GetMessage(ex);
            }

            return response;
        }

        [HttpGet, HttpOptions]
        public virtual Response<TEntity> Get([FromQuery] int Id)
        {
            var response = new Response<TEntity>();

            try
            {
                response.Success = true;
                response.Data = business.Get(Id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ExceptionHelper.GetMessage(ex);
            }

            return response;
        }

        [HttpDelete, HttpOptions]
        public virtual Response Delete([FromQuery] int Id)
        {
            var response = new Response();

            try
            {
                business.Delete(Id);

                response.Success = true;
                response.Data = null;
                response.Message = "Registro excluído com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ExceptionHelper.GetMessage(ex);
            }

            return response;
        }

        [HttpPut, HttpOptions]
        public virtual Response<TEntity> Put([FromQuery] int Id, [FromBody] TEntity entity)
        {
            var response = new Response<TEntity>();

            try
            {
                response.Success = true;
                response.Data = business.Update(entity);
                response.Message = "Registro atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ExceptionHelper.GetMessage(ex);
            }

            return response;
        }

        [HttpPost, HttpOptions]
        public virtual Response<TEntity> Post([FromBody] TEntity entity)
        {
            var response = new Response<TEntity>();

            try
            {
                response.Success = true;
                response.Data = business.Save(entity);
                response.Message = "Registro adicionado com sucesso.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ExceptionHelper.GetMessage(ex);
            }

            return response;
        }

        [Route("GetCount"), HttpGet, HttpOptions]
        public virtual Response GetCount()
        {
            var response = new Response();

            try
            {
                var count = business.Get().Count;

                response.Success = true;
                response.Data = new { Count = count };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ExceptionHelper.GetMessage(ex);
            }

            return response;
        }

        [Route("Paginated"), HttpPost, HttpOptions]
        public virtual Response<PaginateResponse<TEntity>> GetPaginated([FromBody] TFilter filter)
        {
            var response = new Response<PaginateResponse<TEntity>>();

            try
            {
                response.Success = true;
                response.Data = business.Paginate(filter);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ExceptionHelper.GetMessage(ex);
            }

            return response;
        }

        #endregion Public Methods
    }
}