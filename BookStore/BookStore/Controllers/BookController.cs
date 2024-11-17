using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;
        private readonly LanguageRepository _LanguageRepository;

        [ViewData]
        public string Title { get; set; }

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository)
        {
            _bookRepository = bookRepository;
            _LanguageRepository = languageRepository;
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

        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
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

        public async Task<ViewResult> BookSubmit(bool isSuccess = false)
        {
            //var model = new BookModel();
            //model.Language = await _LanguageRepository.GetLanguages();
            //ViewBag.Language = await _LanguageRepository.GetLanguages();
            ViewBag.Language = new SelectList(await _LanguageRepository.GetLanguages(), "Id", "Name");
            ViewBag.IsSuccess = isSuccess;
            Title = "BookSubmit";
            //return View("BookSubmit", model);
            return View("BookSubmit");
        }


        [HttpPost]
        public async Task<IActionResult> BookSubmit(BookModel bookmodel)
        {
            var model = new BookModel();
            Title = "BookSubmit";
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookmodel);
                if (id > 0)
                {
                    return RedirectToAction("BookSubmit", new { isSuccess = true });
                }
            }

            ViewBag.Language = new SelectList(await _LanguageRepository.GetLanguages(), "Id", "Name");

            return View("BookSubmit");
            //model.Language = await _LanguageRepository.GetLanguages();
            //ViewBag.Language = await _LanguageRepository.GetLanguages();
            //ViewBag.LanguageConnectionId = new SelectList(await _LanguageRepository.GetLanguages(), "Id", "Name");
        }

    }
}
