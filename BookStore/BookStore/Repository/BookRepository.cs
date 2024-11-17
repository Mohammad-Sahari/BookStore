using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository
    {
        //Dependency Injection
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            //database connection
            _context = context;
            //automapper
            _mapper = mapper;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                LanguageId = model.LangId,
                UpdatedOn = DateTime.UtcNow
            };
            await _context.Books.AddAsync(newBook);
           await _context.SaveChangesAsync();
            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            //mapping manually

            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if (allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Id = book.Id,
                        LangId = book.LanguageId,
                        LanguageName = book.Language.Name,
                        Title = book.Title,
                        TotalPages = book.TotalPages
                    });
                }
            }
            return books;


            //mapping using automapper

            //var allbooks = await _context.Books.ToListAsync();
            //var books = _mapper.Map<List<BookModel>>(allbooks);
            //return books;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id).Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                LangId = book.LanguageId,
                LanguageName = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages
            }).FirstOrDefaultAsync();
            //var allbooks = await _context.Books.FindAsync(id);
            //var allbooks = await _context.Books.FirstOrDefaultAsync();
            //var book = _mapper.Map<BookModel>(allbooks);
            //return book;
            //return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<BookModel> SearchBook(string title, string author)
        {
            return null;
        }

        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel() { Id = 1, Title = "MVC Book", Author = "Mohammad", Category = "WebDevelopment", LanguageConnectionId = "English", TotalPages = 1000, Description = "This is description for MVC Book"},
        //        new BookModel() { Id = 2, Title = "ASP.NET Book", Author = "Mohammad", Category = "Framework", TotalPages = 980, LanguageConnectionId = "Farsi", Description = "This is description for ASP.NET Book"},
        //        new BookModel() { Id = 3, Title = "C# Begginer", Author = "Amirfazel", Category = "Programming LanguageConnectionId", LanguageConnectionId = "Engish", TotalPages = 800, Description = "This is description for C# begginer Book"},
        //        new BookModel() { Id = 4, Title = "C# Intermediate", Author = "Amirfazel", Category = "Programming LanguageConnectionId",LanguageConnectionId = "English",TotalPages = 890, Description = "This is description for C# intermediate Book"},
        //        new BookModel() { Id = 5, Title = "C# Advanced", Author = "Mohammad",Category = "Programming LanguageConnectionId",LanguageConnectionId = "Farsi",TotalPages = 1500,Description = "This is description for C# advanced Book"},
        //        new BookModel() { Id = 6, Title = "Onion Design Pattern", Author = "Mohammad",Category = "Design Pattern",LanguageConnectionId = "English",TotalPages = 700, Description = "This is description for onion design pattern Book"},
        //    };
        //}
    }
}
