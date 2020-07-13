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
    public class UserController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IUserBusiness userBusiness;

        public UserController(ILogger<BookController> logger, IUserBusiness userBusiness)
        {
            this.logger = logger;
            this.userBusiness = userBusiness;
        }

        /// <summary>
        /// List all users.
        /// </summary>
        /// <returns>List of users</returns>
        /// <response code="200">Returns the list all users</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "manager")]
        [Produces("application/json")]
        [HttpGet(RoutesConfiguration.User.GET_ALL)]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation(LoggingEvents.GetItem, "Getting all items");

            return new JsonResult(await userBusiness.GetAll());
        }

        /// <summary>
        /// Select a specific user.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Specific user</returns>
        /// <response code="200">Returns the specific user</response>
        /// <response code="404">If the user is not found</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "manager")]
        [Produces("application/json")]
        [HttpGet(RoutesConfiguration.User.GET_BY_ID, Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            logger.LogInformation(LoggingEvents.GetItem, "Getting item {Id}", id);

            var item = await userBusiness.Get(id);
            if (item == null)
            {
                logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({Id}) NOT FOUND", id);
                return NotFound();
            }

            logger.LogInformation(LoggingEvents.GetItem, "Geted item {Id}", id);

            return new JsonResult(item);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Returns the created user</returns>
        /// <response code="200">Returns the created user</response>
        /// <response code="400">If the book exists</response>   
        /// <response code="401">If not authorized to perform the action</response> 
        /// <response code="500">Internal server error</response> 
        [Authorize(Roles = "manager")]
        [Produces("application/json")]
        [HttpPost(RoutesConfiguration.User.POST)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] UserInputDto dto)
        {
            logger.LogInformation(LoggingEvents.GetItem, "Creating item {Title}", dto.Username);

            var item = await userBusiness.Create(dto);
            if (item == null)
            {
                logger.LogWarning(LoggingEvents.GetItemNotFound, "Create({Title}) FOUND", dto.Username);
                return BadRequest();
            }

            logger.LogInformation(LoggingEvents.GetItem, "Created item {id}", item.Id);

            return CreatedAtRoute("GetUserById", new { item.Id }, item);
        }

        /// <summary>
        /// Updates a exists user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <response code="204">User successfully updated</response>
        /// <response code="401">If not authorized to perform the action</response>
        /// <response code="404">If the user was not found</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "manager")]
        [Produces("application/json")]
        [HttpPut(RoutesConfiguration.User.PUT)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(Guid id, UserInputDto dto)
        {
            logger.LogInformation(LoggingEvents.GetItem, "Updating item {id}", id);

            var item = await userBusiness.Update(id, dto);
            if (!item)
            {
                logger.LogWarning(LoggingEvents.GetItemNotFound, "Update({Id}) NOT FOUND", id);
                return NotFound();
            }

            logger.LogInformation(LoggingEvents.GetItem, "Updated item {id}", id);

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a exists user.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">User successfully removed</response>
        /// <response code="401">If not authorized to perform the action</response>
        /// <response code="404">If the user was not found</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "manager")]
        [Produces("application/json")]
        [HttpDelete(RoutesConfiguration.User.DELETE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogInformation(LoggingEvents.GetItem, "Deleting item {id}", id);

            var item = await userBusiness.Delete(id);
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