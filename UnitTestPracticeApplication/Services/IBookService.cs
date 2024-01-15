using System;
using UnitTestPracticeApplication.DTOs;
using UnitTestPracticeApplication.Models;

namespace UnitTestPracticeApplication.Services
{
    public interface IBookService
    {
        ResponseDto AddBook(Book book);
        Book GetBook(string title);
        void RemoveBook(Guid id);
    }
}
