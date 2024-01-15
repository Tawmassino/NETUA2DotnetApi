using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NETUA2DotnetApi.Controllers;
using NETUA2DotnetApi.DataLayer.Models;
using NETUA2DotnetApi.DataLayer.Repositories;
using NETUA2DotnetApi.Dtos;
using NETUA2DotnetApi.Services;

namespace TodoApiTests
{
    public class TodoControllerTests
    {
        private readonly Mock<ITodoRepository> _repository;
        private readonly Mock<ITodoValidationService> _todoValidationService;
        private readonly Mock<IToDoEmailService> _toDoEmailService;
        private readonly Mock<ILogger<TodoController>> _logger;
        private readonly ITodoMapper _mapper;

        public TodoControllerTests()
        {
            _repository = new Mock<ITodoRepository>();
            _todoValidationService = new Mock<ITodoValidationService>();
            _toDoEmailService = new Mock<IToDoEmailService>();
            _logger = new Mock<ILogger<TodoController>>();
            _mapper = new TodoMapper();
        }
        [Fact]
        public void GetAll_ShouldReturnOk_Test()
        {
            //arrange
            List<TodoItem> fake = new List<TodoItem>
            {
                new TodoItem { Id = 1, Content = "test", EndDate = DateTime.Now.AddDays(1),Type = "Work" },
                new TodoItem { Id = 2, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Home"},
                new TodoItem { Id = 3, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Hobby"},
                new TodoItem { Id = 4, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Other"}
            };
            _repository.Setup(x => x.GetAll()).Returns(fake);
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.GetAll();
            //assert
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var actualTodo = okResult.Value as IEnumerable<GetToDoItemDto>;
            Assert.Equal(4, actualTodo.Count());
        }
        [Fact]
        public void GetById_ShouldReturnOk_Test()
        {
            //arrange
            var fake = new TodoItem { Id = 1, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Work" };
            _repository.Setup(x => x.GetById(1)).Returns(fake);
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.GetById(1);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var actualTodo = Assert.IsType<GetToDoItemDto>(okResult.Value);
            Assert.Equal(1, actualTodo.Id);

        }
        [Fact]
        public void GetById_ShouldReturnNotFound_Test()
        {
            var fake = new TodoItem { Id = 1, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Work" };
            _repository.Setup(x => x.GetById(1)).Returns(fake);
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.GetById(2);
            //assert
            Assert.IsType<NotFoundResult>(actual);
        }

        [Fact]
        public void FilterBy_null_ShouldReturnOk_Test()
        {
            //arrange
            List<TodoItem> fake = new List<TodoItem>
            {
                new TodoItem { Id = 1, Content = "test", EndDate = DateTime.Now.AddDays(1),Type = "Work" },
                new TodoItem { Id = 2, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Home"},
                new TodoItem { Id = 3, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Hobby"},
                new TodoItem { Id = 4, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Other"}
            };
            _repository.Setup(x => x.GetAll()).Returns(fake);
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.FilterBy(null, null);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var actualTodo = okResult.Value as IEnumerable<GetToDoItemDto>;
            Assert.Equal(4, actualTodo.Count());
        }

        [Fact]
        public void FilterBy_Work_ShouldReturnOk_Test()
        {
            //arrange
            List<TodoItem> fake = new List<TodoItem>
            {
                new TodoItem { Id = 1, Content = "test", EndDate = DateTime.Now.AddDays(1),Type = "Work" },
                new TodoItem { Id = 2, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Home"},
                new TodoItem { Id = 3, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Hobby"},
                new TodoItem { Id = 4, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Other"},
                new TodoItem { Id = 5, Content = "test2", EndDate = DateTime.Now.AddDays(1),Type = "Work" },
            };
            _repository.Setup(x => x.GetAll()).Returns(fake);
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.FilterBy("Work", null);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var actualTodo = okResult.Value as IEnumerable<GetToDoItemDto>;
            Assert.Equal(2, actualTodo.Count());
        }

        [Fact]
        public void FilterBy_test2_ShouldReturnOk_Test()
        {
            //arrange
            List<TodoItem> fake = new List<TodoItem>
            {
                new TodoItem { Id = 1, Content = "test", EndDate = DateTime.Now.AddDays(1),Type = "Work" },
                new TodoItem { Id = 2, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Home"},
                new TodoItem { Id = 3, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Hobby"},
                new TodoItem { Id = 4, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Other"},
                new TodoItem { Id = 5, Content = "test2", EndDate = DateTime.Now.AddDays(1),Type = "Work" },
            };
            _repository.Setup(x => x.GetAll()).Returns(fake);
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.FilterBy(null, "test2");
            //assert
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var actualTodo = okResult.Value as IEnumerable<GetToDoItemDto>;
            Assert.Equal(1, actualTodo.Count());
        }

        [Fact]
        public void FilterBy_Work_test2_ShouldReturnOk_Test()
        {
            //arrange
            List<TodoItem> fake = new List<TodoItem>
            {
                new TodoItem { Id = 1, Content = "test", EndDate = DateTime.Now.AddDays(1),Type = "Work" },
                new TodoItem { Id = 2, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Home"},
                new TodoItem { Id = 3, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Hobby"},
                new TodoItem { Id = 4, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Other"},
                new TodoItem { Id = 5, Content = "test2", EndDate = DateTime.Now.AddDays(1),Type = "Work" },
            };
            _repository.Setup(x => x.GetAll()).Returns(fake);
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.FilterBy("Work", "test2");
            //assert
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var actualTodo = okResult.Value as IEnumerable<GetToDoItemDto>;
            Assert.Equal(1, actualTodo.Count());

        }

        [Fact]
        public void FilterBy_Hobby_test2_ShouldReturnOk_Test()
        {
            //arrange
            List<TodoItem> fake = new List<TodoItem>
            {
                new TodoItem { Id = 1, Content = "test", EndDate = DateTime.Now.AddDays(1),Type = "Work" },
                new TodoItem { Id = 2, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Home"},
                new TodoItem { Id = 3, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Hobby"},
                new TodoItem { Id = 4, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Other"},
                new TodoItem { Id = 5, Content = "test2", EndDate = DateTime.Now.AddDays(1),Type = "Work" },
            };
            _repository.Setup(x => x.GetAll()).Returns(fake);
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.FilterBy("Hobby", "test2");
            //assert
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var actualTodo = okResult.Value as IEnumerable<GetToDoItemDto>;
            Assert.Equal(0, actualTodo.Count());
        }

        [Fact]
        public void Post_ShouldReturnCreated_Test()
        {
            //arrange
            var fake = new CreateToDoItemDto
            {
                Type = "Work",
                Content = "test",
                EndDate = DateTime.Now.AddDays(1)
            };
            _todoValidationService.Setup(x => x.IsValid(fake)).Returns(true);
            _repository.Setup(x => x.Add(It.IsAny<TodoItem>()));
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.Post(fake);
            //assert
            Assert.IsType<CreatedResult>(actual);
            _toDoEmailService.Verify(x => x.TrySendEmail(It.IsAny<string>(), It.IsAny<TodoItem>()), Times.Once);
        }

        [Fact]
        public void Post_ShouldReturnBadRequest_Test()
        {
            //arrange
            var fakeDto = new CreateToDoItemDto { Type = "Work", Content = "test", EndDate = DateTime.Now.AddDays(1) };
            _todoValidationService.Setup(x => x.IsValid(fakeDto)).Returns(false);
            _repository.Setup(x => x.Add(It.IsAny<TodoItem>()));
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.Post(fakeDto);
            //assert
            Assert.IsType<BadRequestObjectResult>(actual);
            _toDoEmailService.Verify(x => x.TrySendEmail(It.IsAny<string>(), It.IsAny<TodoItem>()), Times.Never);
        }

        [Fact]
        public void Put_ShouldReturnNoContent_Test()
        {
            //arrange
            var fakeDto = new UpdateToDoItemDto { Type = "Work", Content = "test", EndDate = DateTime.Now.AddDays(1) };
            var fakeModel = new TodoItem { Id = 1, Content = "test", EndDate = DateTime.Now.AddDays(1), Type = "Work" };
            _todoValidationService.Setup(x => x.IsValid(fakeDto)).Returns(true);
            _repository.Setup(x => x.Update(It.IsAny<TodoItem>()));
            _repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(fakeModel);
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.Put(1, fakeDto);
            //assert
            Assert.IsType<NoContentResult>(actual);
            _toDoEmailService.Verify(x => x.TrySendEmail(It.IsAny<string>(), It.IsAny<TodoItem>()), Times.Once);

        }
        [Fact]
        public void Put_ShouldReturnNotFound_Test()
        {
            //arrange
            var fakeDto = new UpdateToDoItemDto { Type = "Work", Content = "test", EndDate = DateTime.Now.AddDays(1) };
            TodoItem fakeModel = null;
            _todoValidationService.Setup(x => x.IsValid(fakeDto)).Returns(true);
            _repository.Setup(x => x.Update(It.IsAny<TodoItem>()));
            _repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(fakeModel);
            var sut = new TodoController(_repository.Object, _toDoEmailService.Object, _mapper, _todoValidationService.Object, _logger.Object);
            //act
            var actual = sut.Put(2, fakeDto);
            //assert
            Assert.IsType<NotFoundResult>(actual);
            _toDoEmailService.Verify(x => x.TrySendEmail(It.IsAny<string>(), It.IsAny<TodoItem>()), Times.Never);

        }

    }
}
