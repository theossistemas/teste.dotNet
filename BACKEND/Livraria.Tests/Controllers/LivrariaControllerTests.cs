using Livraria.Controllers;
using Livraria.Service.Interfaces;
using Livraria.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Livraria.Tests.Controllers
{
    public class LivrariaControllerTests
    {
        private readonly Mock<ILivroReadService> _readServiceMock;
        private readonly Mock<ILivroCreateService> _createServiceMock;
        private readonly Mock<ILivroUpdateService> _updateServiceMock;
        private readonly Mock<ILivroDeleteService> _deleteServiceMock;
        private readonly LivrariaController _controller;

        public LivrariaControllerTests()
        {
            _readServiceMock = new Mock<ILivroReadService>();
            _createServiceMock = new Mock<ILivroCreateService>();
            _updateServiceMock = new Mock<ILivroUpdateService>();
            _deleteServiceMock = new Mock<ILivroDeleteService>();

            _controller = new LivrariaController(
                _createServiceMock.Object,
                _readServiceMock.Object,
                _updateServiceMock.Object,
                _deleteServiceMock.Object
            );
        }

        [Fact]
        public async Task GetLivroById_DeveRetornarOk_QuandoLivroExiste()
        {
            var livroDto = new LivroDTO { Id = 1, Titulo = "Teste" };
            _readServiceMock.Setup(s => s.GetLivroByIdAsync(1)).ReturnsAsync(livroDto);

            var result = await _controller.GetLivro(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(livroDto, okResult.Value);
        }

        [Fact]
        public async Task GetLivroById_DeveRetornarNotFound_QuandoLivroNaoExiste()
        {
            _readServiceMock.Setup(s => s.GetLivroByIdAsync(1)).ReturnsAsync((LivroDTO?)null);

            var result = await _controller.GetLivro(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddLivro_DeveRetornarCreated_QuandoCriacaoBemSucedida()
        {
            var livroDto = new LivroDTO { Id = 1, Titulo = "Novo Livro" };
            _createServiceMock.Setup(s => s.AddLivroAsync(It.IsAny<LivroDTO>())).ReturnsAsync(livroDto);

            var result = await _controller.PostLivro(livroDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(livroDto, createdResult.Value);
        }

        [Fact]
        public async Task AddLivro_DeveRetornarBadRequest_QuandoCriacaoFalha()
        {
            _createServiceMock.Setup(s => s.AddLivroAsync(It.IsAny<LivroDTO>())).ReturnsAsync((LivroDTO?)null);

            var result = await _controller.PostLivro(new LivroDTO());

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateLivro_DeveRetornarOk_QuandoAtualizacaoBemSucedida()
        {
            var livroDto = new LivroDTO { Id = 1, Titulo = "Atualizado" };
            _updateServiceMock.Setup(s => s.UpdateLivroAsync(1, It.IsAny<LivroDTO>())).ReturnsAsync(livroDto);

            var result = await _controller.PutLivro(1, livroDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(livroDto, okResult.Value);
        }

        [Fact]
        public async Task UpdateLivro_DeveRetornarNotFound_QuandoLivroNaoExiste()
        {
            _updateServiceMock.Setup(s => s.UpdateLivroAsync(1, It.IsAny<LivroDTO>())).ReturnsAsync((LivroDTO?)null);

            var result = await _controller.PutLivro(1, new LivroDTO());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteLivro_DeveRetornarNoContent_QuandoExclusaoBemSucedida()
        {
            _deleteServiceMock.Setup(s => s.DeleteLivroAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteLivro(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteLivro_DeveRetornarNotFound_QuandoExclusaoFalha()
        {
            _deleteServiceMock.Setup(s => s.DeleteLivroAsync(1)).ReturnsAsync(false);

            var result = await _controller.DeleteLivro(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}