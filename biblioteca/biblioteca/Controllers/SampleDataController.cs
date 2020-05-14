using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using biblioteca.Services;
using Microsoft.Extensions.Configuration;
using biblioteca.models;
using Microsoft.AspNetCore.Authorization;

namespace biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleDataController : Controller
    {

        private readonly IConfiguration _config;

        public SampleDataController(IConfiguration configuration)
        {
            _config = configuration;
        }

        private BookServices bookServicesInstance;
        private UserServices userServicesInstance;
        private TokenServices tokenServicesInstance;

        //singletons feito para não ter que instanciar toda vez a classe em uma nova chamada
        private BookServices GetBookServices()
        {
            if (bookServicesInstance == null)
            {
                bookServicesInstance = new BookServices();
                return bookServicesInstance;
            }
            return bookServicesInstance;
        }

        private UserServices GetUserServices()
        {
            if (userServicesInstance == null)
            {
                userServicesInstance = new UserServices();
                return userServicesInstance;
            }
            return userServicesInstance;
        }

        private TokenServices GetTokenServices()
        {
            if (tokenServicesInstance == null)
            {
                tokenServicesInstance = new TokenServices();
                return tokenServicesInstance;
            }
            return tokenServicesInstance;
        }

        [HttpGet("[action]")]
        public User GetUser([FromBody]User user)
        {

            return GetUserServices().GetUserByNamePassword(user, _config);

        }

        [HttpGet("[action]")]
        public String GetToken([FromBody]User user)
        {
            User userFinder = GetUserServices().GetUserByNamePassword(user, _config);

            if (userFinder != null)
            {
                return GetTokenServices().GenerateToken(userFinder, _config);
            }

            return "nao encontrado";
    
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public IEnumerable<Book> GetBook()
        {

            return GetBookServices().GetAllBooks(_config);

        }

        [HttpPost("[action]")]
        [Authorize]
        public JsonResult PostBook([FromBody]Book newBook)
        {
            return GetBookServices().AddBook(newBook, _config);
        }

        [HttpPut("[action]")]
        [Authorize]
        public JsonResult PutBook([FromBody]Book modifyBook)
        {
            return GetBookServices().UpdteBook(modifyBook, _config);
        }

       
        [HttpDelete("[action]")]
        [Authorize]
        public JsonResult DeleteBook([FromBody]Book bookToDelete)
        {
            return GetBookServices().RemoveBook(bookToDelete, _config);
        }

        
    }
}
