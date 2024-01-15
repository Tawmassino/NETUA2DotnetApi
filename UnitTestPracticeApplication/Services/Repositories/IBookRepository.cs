using System;
using UnitTestPracticeApplication.Models;

namespace UnitTestPracticeApplication.Services.Repositories
{
    public interface IBookRepository
    {
        Book GetBook(string title);
        void AddBook(Book book);
        void RemoveBook(Guid id);
    }
}
