using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheosLivraria.Application.UseCases.Book.Create;
using TheosLivraria.Application.UseCases.Book.Delete;
using TheosLivraria.Application.UseCases.Book.GetAll;
using TheosLivraria.Application.UseCases.Book.Update;
using TheosLivraria.Application.UseCases.ViewModels.Common.Response;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.CreateBook;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.GetAllPaged;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.Update;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.Create;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAllPaged;
using TheosLivraria.Domain.Common.Consts.Roles;
using TheosLivraria.Domain.Interfaces.UseCases;

namespace TheosLivraria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        /// <summary>
        /// Retrieves a paginated list of books based on the specified filter criteria.
        /// </summary>
        /// <remarks>This method is an HTTP GET endpoint and expects the filter criteria to be provided as
        /// query parameters.</remarks>
        /// <param name="useCase">The use case responsible for executing the operation to retrieve the paginated list of books. This parameter
        /// is resolved from the service container.</param>
        /// <param name="filter">The filter criteria for pagination and optional search parameters.  Includes details such as page number,
        /// page size, and any additional filtering options.</param>
        /// <returns>An <see cref="IActionResult"/> containing the paginated list of books in the response body. The response is
        /// returned with an HTTP 200 status code.</returns>
        [ProducesResponseType(typeof(ResponsePagedViewModel<ResponseBookAllPagedViewModel>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("paged")]
        public async Task<IActionResult> GetAllPagedAsync([FromServices] IGetAllPagedUseCase useCase, [FromQuery] RequestBookAllPagedViewModel filter )
        {
            return Ok(await useCase.ExecuteAsync(filter));
        }

        /// <summary>
        ///  Gets all books.
        /// </summary>
        /// <remarks>This method is an HTTP GET endpoint then return all books.</remarks>
        /// <param name="useCase">param of service injection to execute the method. </param>
        /// <returns>return list with all books registred in the database.</returns>

        [ProducesResponseType(typeof(IEnumerable<ResponseBookAllPagedViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromServices] IGetAllUseCase useCase)
        {
            return Ok(await useCase.ExecuteAsync());
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <remarks>
        /// This method is an HTTP POST endpoint. It receives a book creation request,
        /// validates if the book already exists, and persists it in the database.
        /// </remarks>
        /// <param name="useCase">The use case service injected to execute the creation logic.</param>
        /// <param name="request">The request model containing the book information to be created.</param>
        /// <returns>Returns the created book wrapped in a ResponseCreateBookViewModel with HTTP 201 status.</returns>
        [ProducesResponseType(typeof(ResponseCreateBookViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = RolesConst.Admin)]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromServices] ICreateBookUseCase useCase, RequestCreateBookViewModel request )
        {
            return Created(string.Empty, await useCase.Execute(request));
        }

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <remarks>
        /// This method is an HTTP PUT endpoint. It receives the book ID and the update request,
        /// validates the data, and updates the corresponding book in the database.
        /// </remarks>
        /// <param name="useCase">The use case service injected to execute the update logic.</param>
        /// <param name="Id">The unique identifier of the book to be updated.</param>
        /// <param name="request">The request model containing the updated book information.</param>
        /// <returns>Returns HTTP 204 No Content if the update is successful.</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = RolesConst.Admin)]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromServices] IUpdateBookUseCase useCase, int Id, [FromBody] RequestUpdateBookViewModel request)
        {
            await useCase.Execute(Id, request);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing book.
        /// </summary>
        /// <remarks>
        /// This method is an HTTP DELETE endpoint. It receives the book ID,
        /// validates if the book exists, and removes it from the database.
        /// </remarks>
        /// <param name="useCase">The use case service injected to execute the delete logic.</param>
        /// <param name="Id">The unique identifier of the book to be deleted.</param>
        /// <returns>Returns HTTP 204 No Content if the deletion is successful.</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = RolesConst.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromServices] IDeleteBookUseCase useCase, int Id)
        {
            await useCase.Execute(Id);
            return NoContent();
        }
    }
}
