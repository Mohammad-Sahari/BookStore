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
                new BookModel() { Id = 1, Title = "MVC Book", Author = "Mohammad" },
                new BookModel() { Id = 2, Title = "ASP.NET Book", Author = "Mohammad" },
                new BookModel() { Id = 3, Title = "C# Begginer", Author = "Amirfazel" },
                new BookModel() { Id = 4, Title = "C# Intermediate", Author = "Amirfazel" },
                new BookModel() { Id = 5, Title = "C# Advanced", Author = "Mohammad" },
                new BookModel() { Id = 6, Title = "Onion Design Pattern Advanced", Author = "Mohammad" },
            };
        }
    }
}
