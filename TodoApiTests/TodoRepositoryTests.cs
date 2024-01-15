using Microsoft.EntityFrameworkCore;
using NETUA2DotnetApi.DataLayer;
using NETUA2DotnetApi.DataLayer.Models;
using NETUA2DotnetApi.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApiTests
{
    public class TodoRepositoryTests
    {
        private TodoContext _context;
        public TodoRepositoryTests()
        {
            //in-memory database
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "todoDatabase" + Guid.NewGuid())
                .Options;
            _context = new TodoContext(options);
            var fakeList = new List<TodoItem>
            {
                new TodoItem {Id=1, Type = "Work", Content = "test", EndDate = DateTime.Now.AddDays(1),UserId = "user" },
                new TodoItem {Id=2, Type = "Home", Content = "test", EndDate = DateTime.Now.AddDays(1),UserId = "user" },
            };
            _context.Todos.RemoveRange(_context.Todos.ToList());
            _context.Todos.AddRange(fakeList);
            _context.SaveChanges();
        }
        [Fact]
        public void GetAll_WhenCalled_ShouldReturnAllItems()
        {
            //arrange
            var sut = new TodoRepository(_context);
            //act
            var actual = sut.GetAll();
            //assert
            Assert.Equal(2, actual.Count());
        }

        [Fact]
        public void GetAll_WhenDbIsEmpty_ShouldReturnEmptyList()
        {
            //arrange
            _context.Todos.RemoveRange(_context.Todos.ToList());
            _context.SaveChanges();
            var sut = new TodoRepository(_context);
            //act
            var actual = sut.GetAll();
            //assert
            Assert.Empty(actual);
        }

        [Fact]
        public void Get_WhenIdIs1_ShouldReturnItem()
        {
            //arrange
            var sut = new TodoRepository(_context);
            //act
            var actual = sut.GetById(1);
            //assert
            Assert.NotNull(actual);

            //Assert.Equal(1, actual.Id);
            //Assert.Equal("Work", actual.Type);
            //Assert.Equal("test", actual.Content);
            //Assert.Equal("user", actual.UserId);
        }

        [Fact]
        public void Get_WhenIdIs10_ShouldReturnNull()
        {
            //arrange
            var sut = new TodoRepository(_context);
            //act
            var actual = sut.GetById(10);
            //assert
            Assert.Null(actual);
        }

        [Fact]
        public void Add_ShouldReturn3_DbShouldContain3Items()
        {
            //arrange
            var sut = new TodoRepository(_context);
            var fake = new TodoItem { Id = 3, Type = "Work", Content = "test", EndDate = DateTime.Now.AddDays(1), UserId = "user" };
            //act
            var actualId = sut.Add(fake);
            var actualDb = sut.GetAll();
            //assert
            Assert.Equal(3, actualId);
            Assert.Equal(3, actualDb.Count());
        }
        [Fact]
        public void Add_WhenAddingItemWithSameId_ShouldThrowError()
        {
            //arrange
            var sut = new TodoRepository(_context);
            var item = new TodoItem { Id = 1, Type = "Work", Content = "Test4", UserId = "1" };
            //act
            //assert
            Assert.Throws<InvalidOperationException>(() => sut.Add(item));
        }
    }
}
