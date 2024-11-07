using System.Collections.Generic;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;
        [ViewData]
        public string Title { get; set; }

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ViewResult GetAllBooks()
        {
            var data = _bookRepository.GetAllBooks();
            return View(data);
        }

        //public List<BookModel> GetAllBooks()
        //{
        //    return _bookRepository.GetAllBooks();
        //}

        public ViewResult GetBook(int id)
        {
            var data = _bookRepository.GetBookById(id);
            return View(data);
        }
        //public BookModel GetBook(int id)
        //{
        //    return _bookRepository.GetBookById(id);
        //}

        public List<BookModel> BookSearch(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName,authorName);
        }

        public ViewResult BookSubmit()
        {
            Title = "BookSubmit";
            return View("BookSubmit");
        }


        [HttpPost]
        public IActionResult BookSumbit(BookModel bookmodel)
        {
            Title = "BookSubmit";
            _bookRepository.AddNewBook(bookmodel);
            int id = _bookRepository.AddNewBook(bookmodel);
               if (id > 0)
            {
                return RedirectToAction("BookSubmit");
            }
            return View("BookSubmit");
        }
    }
}
