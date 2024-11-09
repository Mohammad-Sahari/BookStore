using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
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

        public ViewResult BookSubmit(bool isSuccess = false)
        {
            ViewBag.IsSuccess = isSuccess;
            Title = "BookSubmit";
            return View("BookSubmit");
        }


        [HttpPost]
        public async Task<IActionResult> BookSumbit(BookModel bookmodel)
        {
            Title = "BookSubmit";
           int id = await _bookRepository.AddNewBook(bookmodel);
               if (id > 0)
               {
                return RedirectToAction("BookSubmit", new{isSuccess = true});
               }
            return View("BookSubmit");
        }
    }
}
