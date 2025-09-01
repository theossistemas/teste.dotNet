
using Microsoft.AspNetCore.Mvc;
using Moq;
using TheosLivraria.API.Controllers;
using TheosLivraria.Application.UseCases.Book.Create;
using TheosLivraria.Application.UseCases.Book.Delete;
using TheosLivraria.Application.UseCases.Book.GetAll;
using TheosLivraria.Application.UseCases.Book.Update;
using TheosLivraria.Application.UseCases.ViewModels.Common.Response;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.CreateBook;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.GetAllPaged;
using TheosLivraria.Application.UseCases.ViewModels.Request.Book.Update;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.Create;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAll;
using TheosLivraria.Application.UseCases.ViewModels.Response.Book.GetAllPaged;
using TheosLivraria.Domain.Interfaces.UseCases;
using Xunit;

namespace TheosLivraria.Integration.Tests.Controller
{
    public class BookControllerTests
    {
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _controller = new BookController();
        }

        [Fact]
        public async Task GetAllPagedAsync_ShouldReturnOk()
        {
            // Arrange
            var mockUseCase = new Mock<IGetAllPagedUseCase>();
            var request = new RequestBookAllPagedViewModel { Page = 1, Size = 10 };
            var response = new ResponsePagedViewModel<ResponseBookAllPagedViewModel>(
                new List<ResponseBookAllPagedViewModel>(), 0, request.Page, request.Size);

            mockUseCase.Setup(x => x.ExecuteAsync(request))
                       .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllPagedAsync(mockUseCase.Object, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
            mockUseCase.Verify(x => x.ExecuteAsync(request), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOk()
        {
            // Arrange
            var mockUseCase = new Mock<IGetAllUseCase>();
            var data = new List<ResponseBookAllViewModel>();
            mockUseCase.Setup(x => x.ExecuteAsync()).ReturnsAsync(data);

            // Act
            var result = await _controller.GetAllAsync(mockUseCase.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(data, okResult.Value);
            mockUseCase.Verify(x => x.ExecuteAsync(), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnCreated()
        {
            // Arrange
            var mockUseCase = new Mock<ICreateBookUseCase>();
            var request = new RequestCreateBookViewModel { Name = "Book 1", Author = "Author 1" };
            var response = new ResponseCreateBookViewModel { Id = 1 };

            mockUseCase.Setup(x => x.Execute(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.PostAsync(mockUseCase.Object, request);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(response, createdResult.Value);
            mockUseCase.Verify(x => x.Execute(request), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnNoContent()
        {
            // Arrange
            var mockUseCase = new Mock<IUpdateBookUseCase>();
            var request = new RequestUpdateBookViewModel { Name = "Updated Book", Author = "Updated Author" };
            int bookId = 1;

            // Act
            var result = await _controller.PutAsync(mockUseCase.Object, bookId, request);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockUseCase.Verify(x => x.Execute(bookId, request), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNoContent()
        {
            // Arrange
            var mockUseCase = new Mock<IDeleteBookUseCase>();
            int bookId = 1;

            // Act
            var result = await _controller.DeleteAsync(mockUseCase.Object, bookId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            mockUseCase.Verify(x => x.Execute(bookId), Times.Once);
        }
    }
}
