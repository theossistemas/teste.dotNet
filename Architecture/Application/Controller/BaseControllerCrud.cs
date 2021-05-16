using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    public abstract class BaseControllerCrud<TDomain, TDTO, TInsertDTO, TUpdateDTO> : BaseController<TDomain, TDTO>
        where TDomain : BaseDomain where TDTO : class where TInsertDTO : class where TUpdateDTO : class
    {
        public BaseControllerCrud(IServiceBase<TDomain> service, IMapper mapper, ILogger<BaseController<TDomain, TDTO>> logger) : base(service, mapper, logger)
        {
        }

        [Authorize]
        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TInsertDTO dto)
        {
            var domain = this._mapper.Map<TDomain>(dto);
            
            this._logger.LogDebug(JsonConvert.SerializeObject(domain));

            await this._service.CreateAsync(domain);
            await this._service.SaveChangeAsync();

            return Ok(true);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var domain = await this._service.GetByIdAsync(id);

            this._logger.LogDebug(JsonConvert.SerializeObject(domain));

            if (domain == null)
            {
                this._logger.LogDebug("Not found!");
                return NotFound();
            }

            await this._service.DeleteAsync(id);
            await this._service.SaveChangeAsync();

            return Ok(true);
        }

        [Authorize]
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TUpdateDTO dto)
        {
            var domain = await this._service.GetByIdAsync(id);

            this._logger.LogDebug(JsonConvert.SerializeObject(domain));

            if (domain == null)
            {
                this._logger.LogDebug("Not found!");
                return NotFound();
            }

            this._mapper.Map(dto, domain);

            this._logger.LogDebug(JsonConvert.SerializeObject(domain));

            await this._service.UpdateAsync(id, domain);
            await this._service.SaveChangeAsync();

            return Ok();
        }

    }
}
