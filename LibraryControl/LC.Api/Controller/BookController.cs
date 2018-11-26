using LC.Aplication.Book;
using LC.Aplication.Book.DataTransferObject;
using LC.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LC.Api.Controller
{
    [Route("api/1.0/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        /// <summary>
        /// Buscar todos os livros cadastrados ordenados de forma ascendente pelo nome;
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get([FromServices] ApplicationBook app)
        {
            try
            {
                return Ok(app.GetOrderedAscendingByName().ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cadastrar livro
        /// </summary>
        /// <param name="createdBookDTO"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Book> Created(CreatedBookDTO createdBookDTO, [FromServices] ApplicationBook app)
        {
            try
            {
                return Ok(app.Created(createdBookDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Atualizar livro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createdBookDTO"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, CreatedBookDTO createdBookDTO, [FromServices] ApplicationBook app)
        {

            try
            {
                return Ok(app.Updated(id, createdBookDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Remover livro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id, [FromServices] ApplicationBook app)
        {
            try
            {
                app.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}