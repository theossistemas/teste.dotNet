using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MMM.Library.Application.ViewModels;
using MMM.Library.Domain.Core.EvetSourcing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Services.AspNetWebApi.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/events")]
    public class StoredEventsController : ControllerBase
    {
        private readonly IEventSourcingRepository _eventStore;
        private readonly IMapper _mapper;

        public StoredEventsController(IEventSourcingRepository eventStore, IMapper mapper)
        {
            _eventStore = eventStore;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StoredEventViewModel>> GetAllEvents()
        {
            return _mapper.Map<IEnumerable<StoredEventViewModel>>(await _eventStore.GetAllEvents());

        }
    }
}
