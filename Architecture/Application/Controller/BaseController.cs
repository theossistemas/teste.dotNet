using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    [EnableCors]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TDomain, TDTO> : ControllerBase
        where TDomain : BaseDomain where TDTO : class
    {
        protected Account _account => HttpContext != null ? (Account)HttpContext.Items["Account"] : null;
        protected readonly IServiceBase<TDomain> _service;
        protected readonly IMapper _mapper;
        protected readonly ILogger<BaseController<TDomain, TDTO>> _logger;

        public BaseController(IServiceBase<TDomain> service, IMapper mapper, ILogger<BaseController<TDomain, TDTO>> logger)
        {
            this._service = service;
            this._mapper = mapper;
            this._logger = logger;
        }

        [Authorize]
        [HttpGet]
        public virtual async Task<ActionResult<ICollection<TDTO>>> GetAll()
        {
            var domains = await this._service.GetAllAsync();

            this._logger.LogDebug(JsonConvert.SerializeObject(domains));

            var result = this._mapper.Map<ICollection<TDTO>>(domains);

            this._logger.LogDebug(JsonConvert.SerializeObject(result));

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDTO>> GetById([FromRoute] Guid id)
        {
            var domain = await this._service.GetByIdAsync(id);

            this._logger.LogDebug(JsonConvert.SerializeObject(domain));

            if (domain == null)
            {
                this._logger.LogDebug("Not found!");
                return NotFound();
            }

            var dto = this._mapper.Map<TDTO>(domain);
            
            this._logger.LogDebug(JsonConvert.SerializeObject(dto));

            return Ok(dto);
        }
    }
}
