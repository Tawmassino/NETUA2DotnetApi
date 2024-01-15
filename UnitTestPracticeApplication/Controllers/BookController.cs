using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using UnitTestPracticeApplication.DTOs;
using UnitTestPracticeApplication.Models;
using UnitTestPracticeApplication.Services;
using UnitTestPracticeApplication.Services.Repositories;

namespace UnitTestPracticeApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]//tiktai autorizuoti
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public ActionResult<Book> GetBooks([FromQuery] string title)
        {
            return _service.GetBook(title);
        }

        [HttpDelete("Remove")]
        public void RemoveBook([FromQuery] Guid id)
        {
            _service.RemoveBook(id);
        }

        [HttpPost("AddBook")]
        public ActionResult<ResponseDto> AddBook(Book book)
        {
            return _service.AddBook(book);
        }
    }
}
