using System;

namespace UnitTestPracticeApplication.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
    }
}
