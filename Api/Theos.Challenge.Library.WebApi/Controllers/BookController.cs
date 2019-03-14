using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Theos.Challenge.Library.Application.Contracts.Services;
using Theos.Challenge.Library.SharedKernel.DTO;
using Theos.Challenge.Library.SharedKernel.ValueObjects;

namespace Theos.Challenge.Library.WebApi.Controllers
{
    public class BookController: Controller
    {
        private readonly IBookAppService _bookAppService;
        private readonly IMapper _mapper;

        public BookController(IBookAppService bookAppService, IMapper mapper)
        {
            _bookAppService = bookAppService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDTO dto){
            return Ok(await _bookAppService.Create(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(BookDTO dto){
            return Ok(await _bookAppService.Update(dto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Identifier id){
            return Ok(await _bookAppService.GetBookByIdentifier(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            return Ok(await _bookAppService.GetAllBooksAsc());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(BookDTO dto){
            return Ok(await _bookAppService.Delete(dto));
        }
    }
}