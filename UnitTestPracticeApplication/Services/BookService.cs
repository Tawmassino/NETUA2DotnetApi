using System;
using UnitTestPracticeApplication.DTOs;
using UnitTestPracticeApplication.Models;
using UnitTestPracticeApplication.Services.Repositories;

namespace UnitTestPracticeApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public ResponseDto AddBook(Book book)
        {
            var existingBook = _repository.GetBook(book.Title);
            if (existingBook is not null)
                return new ResponseDto(false, "Book already exist");

            _repository.AddBook(book);
            return new ResponseDto(true, "");
        }

        public Book GetBook(string title)
        {
            return _repository.GetBook(title);
        } 

        public void RemoveBook(Guid id)
        {
            _repository.RemoveBook(id);
        }
    }
}
