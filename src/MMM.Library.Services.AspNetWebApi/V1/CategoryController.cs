using MediatR;
using Microsoft.AspNetCore.Mvc;
using MMM.Library.Application.Interfaces;
using MMM.Library.Application.ViewModels;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Services.AspNetWebApi.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/category")]
    public class CategoryController : ApiBaseController
    {
        private readonly ICategoryAppService _categoryAppService;
        public CategoryController(INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediatorHandler,
                              ICategoryAppService categoryAppService)
            : base(notifications, mediatorHandler)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            return await _categoryAppService.GetAll();
        }

        [HttpGet]
        [Route("get-by-id/{id:guid}")]
        public async Task<ActionResult<CategoryViewModel>> GetCategoryById(Guid id)
        {
            var categoryViewModel = await _categoryAppService.GetById(id);

            if (categoryViewModel == null) return NotFound();

            return categoryViewModel;
        }

        [HttpPut]
        [Route("update/{id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                NotifyError("Id Inválido", "Erro: Id fornecido não é válido");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _categoryAppService.Update(categoryViewModel);

            return CustomResponse(categoryViewModel);
        }


        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<CategoryViewModel>> AddCategory(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _categoryAppService.Add(categoryViewModel);

            return CustomResponse(categoryViewModel);
        }

        // DELETE: api/Course/5
        [HttpDelete]
        [Route("delete/{id:guid}")]
        public async Task<ActionResult<CategoryViewModel>> DeleteCategory(Guid id)
        {
            var categoryViewModel = await _categoryAppService.GetById(id);

            if (categoryViewModel == null) return NotFound();

            await _categoryAppService.Delete(id);

            return CustomResponse(categoryViewModel);
        }
    }

}
