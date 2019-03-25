using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Theos.Library.Api.Controllers;
using Theos.Library.Api.Mapper;
using Theos.Library.Api.Models.Book;
using Theos.Library.Core.Business;
using Theos.Library.Core.Data.Repository;
using Theos.Library.CrossCutting.Response;
using Theos.Library.CrossCutting.Response.Error;
using Xunit;

namespace Theos.Library.Api.Test
{
    public class BookControllerTest
    {
        private readonly BookController _bookController;

        public BookControllerTest()
        {
            MapperConfig.RegisterMappings();

            var repository = new BookRepository();
            var service = new BookService(repository);
            _bookController = new BookController(service);
        }

        [Fact]
        public void Ok_WhenCalled_Post_Without_Cover()
        {
            var model = new BookInsertModel
            {
                Title = "Book 1",
                Description = "There is a book 1",
                Author = "Author 1"
            };

            var result = _bookController.Post(model);
            Assert.True(result is CreatedResult);
            
        }

        [Fact]
        public void Ok_WhenCalled_Post_With_Cover()
        {
            var model = new BookInsertModel
            {
                Title = "Book 1",
                Description = "There is a book 1",
                Author = "Author 1",
                Cover = "http://localhost/imagem.jpg"
            };

            var result = _bookController.Post(model);
            Assert.True(result is CreatedResult);
            
        }

        [Fact]
        public void BadRequest_WhenCalled_Post_Without_Title()
        {
            var model = new BookInsertModel
            {
                Description = "There is a book 1",
                Author = "Author 1"
            };

            var result = _bookController.Post(model);
            Assert.True(result is BadRequestObjectResult);

            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseModel);

            var resultModel = (ErrorResponseModel)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Title"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
            
        }

        [Fact]
        public void BadRequest_WhenCalled_Post_Without_Description()
        {
            var model = new BookInsertModel
            {
                Title = "Book 1",
                Author = "Author 1"
            };

            var result = _bookController.Post(model);
            Assert.True(result is BadRequestObjectResult);

            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseModel);

            var resultModel = (ErrorResponseModel)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Description"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
            
        }

        [Fact]
        public void BadRequest_WhenCalled_Post_Without_Author()
        {
            var model = new BookInsertModel
            {
                Title = "Book 1",
                Description = "There is a book 1"
            };

            var result = _bookController.Post(model);
            Assert.True(result is BadRequestObjectResult);

            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseModel);

            var resultModel = (ErrorResponseModel)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Author"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
            
        }

        [Fact]
        public void Ok_WhenCalled_Get_Without_Parameters()
        {
            Ok_WhenCalled_Post_Without_Cover();

            var result = _bookController.Get();
            Assert.True(result is OkObjectResult);

            var okResult = (OkObjectResult) result;
            Assert.True(okResult.Value is List<BookListModel>);

            var resultModel = (List<BookListModel>) okResult.Value;
            Assert.True(resultModel.Count > 0);
            
        }

        [Fact]
        public List<BookListModel> Ok_WhenCalled_Get_With_PageControl()
        {
            Ok_WhenCalled_Post_Without_Cover();

            var result = _bookController.Get(1, 100);
            Assert.True(result is OkObjectResult);

            var okResult = (OkObjectResult)result;
            Assert.True(okResult.Value is ResponseModel<BookListModel>);

            var resultModel = (ResponseModel<BookListModel>)okResult.Value;

            Assert.True(resultModel.Data.Count > 0);
            return resultModel.Data.ToList();
            
        }

        [Fact]
        public void Ok_WhenCalled_Get_With_PageControl_Filter()
        {
            Ok_WhenCalled_Post_Without_Cover();

            var result = _bookController.Get(1, 10, "Book 1");
            Assert.True(result is OkObjectResult);

            var okResult = (OkObjectResult)result;
            Assert.True(okResult.Value is ResponseModel<BookListModel>);

            var resultModel = (ResponseModel<BookListModel>)okResult.Value;

            Assert.True(resultModel.Data.Count > 0);
            
        }

        [Fact]
        public BookModel Ok_WhenCalled_Get_With_Id()
        {
            var list = Ok_WhenCalled_Get_With_PageControl();

            var model = list.FirstOrDefault();

            var result = _bookController.Get(model.Id);
            Assert.True(result is OkObjectResult);

            var okResult = (OkObjectResult)result;
            Assert.True(okResult.Value is BookModel);

            return (BookModel)okResult.Value;
            
        }

        [Fact]
        public void NotFound_WhenCalled_Get_Wrong_Id()
        {
            var result = _bookController.Get(Guid.NewGuid());
            Assert.True(result is NotFoundResult);
            
        }

        [Fact]
        public void Ok_WhenCalled_Put_Without_Cover()
        {
            var model = Ok_WhenCalled_Get_With_Id();
            model.Cover = string.Empty;

            var temp = JsonConvert.SerializeObject(model);
            var updateModel = JsonConvert.DeserializeObject<BookUpdateModel>(temp);

            var result = _bookController.Put(model.Id, updateModel);
            Assert.True(result is OkObjectResult);
            
        }

        [Fact]
        public void Ok_WhenCalled_Put_With_Cover()
        {
            var model = Ok_WhenCalled_Get_With_Id();
            model.Cover = "http://localhost/imagem.jpg";

            var temp = JsonConvert.SerializeObject(model);
            var updateModel = JsonConvert.DeserializeObject<BookUpdateModel>(temp);

            var result = _bookController.Put(model.Id, updateModel);
            Assert.True(result is OkObjectResult);
            
        }

        [Fact]
        public void BadRequest_WhenCalled_Put_Without_Title()
        {
            var model = Ok_WhenCalled_Get_With_Id();
            model.Title = string.Empty;

            var temp = JsonConvert.SerializeObject(model);
            var updateModel = JsonConvert.DeserializeObject<BookUpdateModel>(temp);

            var result = _bookController.Put(model.Id, updateModel);
            Assert.True(result is BadRequestObjectResult);

            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseModel);

            var resultModel = (ErrorResponseModel)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Title"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
            
        }

        [Fact]
        public void BadRequest_WhenCalled_Put_Without_Description()
        {
            var model = Ok_WhenCalled_Get_With_Id();
            model.Description = string.Empty;

            var temp = JsonConvert.SerializeObject(model);
            var updateModel = JsonConvert.DeserializeObject<BookUpdateModel>(temp);

            var result = _bookController.Put(model.Id, updateModel);
            Assert.True(result is BadRequestObjectResult);

            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseModel);

            var resultModel = (ErrorResponseModel)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Description"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
            
        }

        [Fact]
        public void BadRequest_WhenCalled_Put_Without_Author()
        {
            var model = Ok_WhenCalled_Get_With_Id();
            model.Author = string.Empty;

            var temp = JsonConvert.SerializeObject(model);
            var updateModel = JsonConvert.DeserializeObject<BookUpdateModel>(temp);

            var result = _bookController.Put(model.Id, updateModel);
            Assert.True(result is BadRequestObjectResult);

            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseModel);

            var resultModel = (ErrorResponseModel)badRequestResult.Value;
            Assert.Equal(resultModel.Errors.Count, 1);
            Assert.True(resultModel.Errors.Any(a => a.Field == "Author"));
            Assert.True(resultModel.Errors.Any(a => a.Messages.All(all => all.ToLower().Contains("required"))));
            
        }

        [Fact]
        public void Ok_WhenCalled_Delete()
        {
            var list = Ok_WhenCalled_Get_With_PageControl();

            var model = list.FirstOrDefault();

            var result = _bookController.Delete(model.Id);
            Assert.True(result is OkResult);
            DeleteAllRegisters();
            
        }

        private void DeleteAllRegisters()
        {
            var list = Ok_WhenCalled_Get_With_PageControl().ToList();
            list.ForEach(item => _bookController.Delete(item.Id));
        }
    }
}
