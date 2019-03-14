using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using livraria.Application.interfaces;
using livraria.Domain.entities;
using livraria.Web.Mappers;
using livraria.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace livraria.Web.Controllers
{
    [Route("api/livro")]
    [ApiController]
    public class LivroController : Controller
    {
        private readonly ILivroApplication _livroApplication;

        public LivroController(ILivroApplication livroApplication)
        {
            _livroApplication = livroApplication;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            try
            {
                var result = _livroApplication.GetAll().IListTo<LivroResponseVM>();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        [Authorize("Bearer")]
        public IActionResult Get(int Id)
        {
            try
            {
                var result = _livroApplication.GetById(Id).MapTo<LivroResponseVM>();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Post(LivroResponseVM livro)
        {
            try
            {
                _livroApplication.Create(livro.MapTo<Livro>());

                return Ok(true);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Authorize("Bearer")]
        public IActionResult Put(LivroRequestVM livroVm)
        {
            try
            {
                _livroApplication.Update(livroVm.Livro.MapTo<Livro>(), livroVm.Id);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize("Bearer")]
        public IActionResult Delete(int Id)
        {
            try
            {
                _livroApplication.Delete(Id);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}