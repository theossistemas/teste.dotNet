using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Theos.Library.Api.Controllers.Base;
using Theos.Library.Api.Models.Book;
using Theos.Library.Api.Security;
using Theos.Library.Core.Business.Interface;
using Theos.Library.CrossCutting.Filter.Book;
using Theos.Library.CrossCutting.Response;
using Theos.Library.CrossCutting.Response.Error;
using Theos.Library.Domain.Books;

namespace Theos.Library.Api.Controllers
{
    [Route("api/v1/books")]
    public class BookController : BaseController<Book, BookFilter, BookModel, BookListModel, BookInsertModel, BookUpdateModel>
    {
        public BookController(IBookService service) : base(service)
        {
        }

        [HttpGet("{id}")]
        [AccessValidation]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(InternalServerErrorResponseModel), (int)HttpStatusCode.InternalServerError)]
        public new ActionResult Get(Guid id)
        {
            return base.Get(id);
        }

        [HttpGet]
        [AccessValidation]
        [ProducesResponseType(typeof(List<BookListModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel<BookListModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerErrorResponseModel), (int)HttpStatusCode.InternalServerError)]
        public ActionResult Get(int? page = null, int? perPage = null, string term = null, bool? ascending = null, BookOrdination? ordination = null)
        {
            var request = new BookFilter(term, ascending, ordination);
            return Get(page, perPage, request);
        }

        [HttpPost]
        [AccessValidation]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorResponseModel), (int)HttpStatusCode.InternalServerError)]
        public ActionResult Post([FromBody] BookInsertModel model)
        {
            return base.Post(model);
        }

        [HttpPut("{id}")]
        [AccessValidation]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorResponseModel), (int)HttpStatusCode.InternalServerError)]
        public new ActionResult Put([FromRoute]Guid id, [FromBody] BookUpdateModel model)
        {
            return base.Put(id, model);
        }


        [HttpDelete("{id}")]
        [AccessValidation]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerErrorResponseModel), (int)HttpStatusCode.InternalServerError)]
        public new ActionResult Delete([FromRoute]Guid id)
        {
            return base.Delete(id);
        }
    }
}
