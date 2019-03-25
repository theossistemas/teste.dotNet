using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Theos.Library.Api.Helper;
using Theos.Library.Api.Models.Base;
using Theos.Library.Core.Business.Interface.Base;
using Theos.Library.CrossCutting.Filter.Base;
using Theos.Library.CrossCutting.Helper;
using Theos.Library.CrossCutting.Request;
using Theos.Library.CrossCutting.Response;

namespace Theos.Library.Api.Controllers.Base
{
    public class BaseController<T, TF, TM, TLm, TIm, TUm> : ControllerBase where T : class
                                                                           where TF : BaseFilter
                                                                           where TLm : class
                                                                           where TM : class
                                                                           where TIm : class
                                                                           where TUm : BaseUpdateModel
    {
        protected readonly IBaseService<T, TF> Service;
        protected List<ValidationResult> ValidationResultList = new List<ValidationResult>();

        public BaseController(IBaseService<T, TF> service)
        {
            Service = service;
        }

        protected bool Validate<TTm>(TTm model) where TTm : class
        {
            return !Validator.TryValidateObject(model, new ValidationContext(model), ValidationResultList);
        }

        protected ActionResult GetErrors()
        {
            var response = BadRequest(ControllerHelper.GetErrors(ValidationResultList));
            ValidationResultList = new List<ValidationResult>();
            return response;
        }

        protected ActionResult Post(TIm model)
        {
            if (Validate(model))
                return GetErrors();

            var response = Insert(model);
            return Created(response);
        }

        protected virtual Guid Insert(TIm model)
        {
            return Service.Create(MapperHelper.Map<TIm, T>(model));
        }

        protected ActionResult Put(Guid id, TUm model)
        {
            model.Id = id;

            if (Validate(model))
                return GetErrors();

            var response = Update(model);
            return Ok(MapperHelper.Map<T, TM>(response));
        }

        protected virtual T Update(TUm model)
        {
            return Service.Update(MapperHelper.Map<TUm, T>(model));
        }

        protected ActionResult Delete(Guid id)
        {
            Service.Remove(id);
            return Ok();
        }

        protected ActionResult Get(Guid id)
        {
            var response = Service.Find(id);
            if (response == null)
                return NotFound();

            return Ok(MapperHelper.Map<T, TM>(response));
        }

        protected ActionResult Get(int? page = null, int? perPage = null, object filter = null)
        {
            if (page.HasValue || perPage.HasValue)
            {
                var response = Service.Search(new RequestModel<TF>(page, perPage, (TF)filter));
                return Ok(MapperHelper.Map<ResponseModel<T>, ResponseModel<TLm>>(response));
            }

            return Ok(MapperHelper.CopyList<T, TLm>(Service.All()));
        }

        protected virtual ActionResult Created(Guid id)
        {
            return Created(string.Empty, id);
        }
    }
}