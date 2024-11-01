using System.Collections.Generic;
using System.Linq;
using BookStore.Models;

namespace BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<BookModel> SearchBook(string title, string author)
        {
            return DataSource().Where(x => x.Title == title && x.Author == author).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel() { Id = 1, Title = "MVC Book", Author = "Mohammad", Category = "WebDevelopment", Language = "English", TotalPages = 1000, Description = "This is description for MVC Book"},
                new BookModel() { Id = 2, Title = "ASP.NET Book", Author = "Mohammad", Category = "Framework", TotalPages = 980, Language = "Farsi", Description = "This is description for ASP.NET Book"},
                new BookModel() { Id = 3, Title = "C# Begginer", Author = "Amirfazel", Category = "Programming Language", Language = "Engish", TotalPages = 800, Description = "This is description for C# begginer Book"},
                new BookModel() { Id = 4, Title = "C# Intermediate", Author = "Amirfazel", Category = "Programming Language",Language = "English",TotalPages = 890, Description = "This is description for C# intermediate Book"},
                new BookModel() { Id = 5, Title = "C# Advanced", Author = "Mohammad",Category = "Programming Language",Language = "Farsi",TotalPages = 1500,Description = "This is description for C# advanced Book"},
                new BookModel() { Id = 6, Title = "Onion Design Pattern", Author = "Mohammad",Category = "Design Pattern",Language = "English",TotalPages = 700, Description = "This is description for onion design pattern Book"},
            };
        }
    }
}
