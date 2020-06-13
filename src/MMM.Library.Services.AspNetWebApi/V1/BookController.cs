using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMM.Library.Application.Interfaces;
using MMM.Library.Application.ViewModels;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Notifications;
using MMM.Library.Domain.CQRS.Queries;
using MMM.Library.Domain.CQRS.Queries.ViewModels;
using MMM.Library.Infra.CrossCutting.Identity.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Services.AspNetWebApi.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/book")]
    public class BookController : ApiBaseController
    {
        private readonly IBookAppService _bookAppService;
        private readonly IBookQueries _bookQueries;
        public BookController(INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediatorHandler,
                              IBookAppService bookAppService,
                              IBookQueries bookQueries)
            : base(notifications, mediatorHandler)
        {
            _bookAppService = bookAppService;
            _bookQueries = bookQueries;
        }

        [AllowAnonymous]
        //[ClaimsAuthorize("Book", "Read")]
        [HttpGet]
        [Route("get-all")]
        public async Task<IEnumerable<BookAndCategoryViewModel>> GetAll()
        {
            // Exemplo Queries CQRS sem AutoMapper
            return await _bookQueries.GetAllBooksWithCategory();
        }

        [AllowAnonymous]
        // [ClaimsAuthorize("Book", "Read")]
        [HttpGet]
        [Route("get-by-id/{id:guid}")]
        public async Task<ActionResult<BookViewModel>> GetBookById(Guid id)
        {
            var bookViewModel = await _bookAppService.GetById(id);

            if (bookViewModel == null) return NotFound();

            return bookViewModel;
        }

        [AllowAnonymous]
        //[ClaimsAuthorize("Book", "Read")]
        [HttpGet]
        [Route("get-by-category/{code:int}")]
        public async Task<IEnumerable<BookViewModel>> GetBookByCategory(int code)
        {
            return await _bookAppService.GetByCategory(code);
        }


        [ClaimsAuthorize("Book", "Add")]
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<BookViewModel>> AddBook(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _bookAppService.AddBook(bookViewModel);

            return CustomResponse(bookViewModel);
        }

        [ClaimsAuthorize("Book", "Update")]
        [HttpPut]
        [Route("update/{id:guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookViewModel bookViewModel)
        {
            if (id != bookViewModel.Id)
            {
                NotifyError("Id Inválido", "Erro: Id fornecido não é válido");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _bookAppService.UpdateBook(bookViewModel);

            return CustomResponse(bookViewModel);
        }

        [ClaimsAuthorize("Book", "Delete")]
        [HttpDelete]
        [Route("delete/{id:guid}")]
        public async Task<ActionResult<BookViewModel>> DeleteBook(Guid id)
        {
            var bookViewModel = await _bookAppService.GetById(id);

            if (bookViewModel == null) return NotFound();

            await _bookAppService.DeleteBook(id);

            return CustomResponse(bookViewModel);
        }       
    }
}
