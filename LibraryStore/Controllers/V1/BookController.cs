using LibraryStore.Configurations;
using LibraryStore.Core.Business;
using LibraryStore.Core.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryStore.Controllers.v1
{
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IBookBusiness bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            this.logger = logger;
            this.bookBusiness = bookBusiness;
        }

        /// <summary>
        /// List all books.
        /// </summary>
        /// <returns>List of books</returns>
        /// <response code="200">Returns the list all books</response>
        /// <response code="500">Internal server error</response>  
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet(RoutesConfiguration.Book.GET_ALL)]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation(LoggingEvents.GetItem, "Getting all items");

            return new JsonResult(await bookBusiness.GetAll());
        }

        /// <summary>
        /// Select a specific book.
        /// </summary>
        /// <param name="id">Book identifier</param>
        /// <returns>Specific book</returns>
        /// <response code="200">Returns the specific book</response>
        /// <response code="404">If the book is not found</response>
        /// <response code="500">Internal server error</response>
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpGet(RoutesConfiguration.Book.GET_BY_ID, Name = "GetBookById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            logger.LogInformation(LoggingEvents.GetItem, "Getting item {Id}", id);

            var item = await bookBusiness.Get(id);
            if (item == null)
            {
                logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({Id}) NOT FOUND", id);
                return NotFound();
            }

            logger.LogInformation(LoggingEvents.GetItem, "Geted item {Id}", id);

            return new JsonResult(item);
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Returns the created book</returns>
        /// <response code="200">Returns the created book</response>
        /// <response code="400">If the book exists</response>   
        /// <response code="401">If not authorized to perform the action</response> 
        /// <response code="500">Internal server error</response> 
        [Authorize(Roles = "manager")]
        [Produces("application/json")]
        [HttpPost(RoutesConfiguration.Book.POST)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] BookInputDto dto)
        {
            logger.LogInformation(LoggingEvents.GetItem, "Creating item {Title}", dto.Title);

            var item = await bookBusiness.Create(dto);
            if (item == null)
            {
                logger.LogWarning(LoggingEvents.GetItemNotFound, "Create({Title}) FOUND", dto.Title);
                return BadRequest();
            }

            logger.LogInformation(LoggingEvents.GetItem, "Created item {id}", item.Id);

            return CreatedAtRoute("GetBookById", new { item.Id }, item);
        }

        /// <summary>
        /// Updates a exists book.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <response code="204">Book successfully updated</response>
        /// <response code="401">If not authorized to perform the action</response>
        /// <response code="404">If the book was not found</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "manager")]
        [Produces("application/json")]
        [HttpPut(RoutesConfiguration.Book.PUT)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(Guid id, BookInputDto dto)
        {
            logger.LogInformation(LoggingEvents.GetItem, "Updating item {id}", id);

            var item = await bookBusiness.Update(id, dto);
            if (!item)
            {
                logger.LogWarning(LoggingEvents.GetItemNotFound, "Update({Id}) NOT FOUND", id);
                return NotFound();
            }

            logger.LogInformation(LoggingEvents.GetItem, "Updated item {id}", id);

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a exists book.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Book successfully removed</response>
        /// <response code="401">If not authorized to perform the action</response>
        /// <response code="404">If the book was not found</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "manager")]
        [Produces("application/json")]
        [HttpDelete(RoutesConfiguration.Book.DELETE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogInformation(LoggingEvents.GetItem, "Deleting item {id}", id);

            var item = await bookBusiness.Delete(id);
            if (!item)
            {
                logger.LogWarning(LoggingEvents.GetItemNotFound, "Delete({Id}) NOT FOUND", id);
                return NotFound();
            }

            logger.LogInformation(LoggingEvents.GetItem, "Deleted item {id}", id);

            return new NoContentResult();
        }
    }
}