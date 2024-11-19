using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;
        private readonly LanguageRepository _LanguageRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [ViewData]
        public string Title { get; set; }

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _LanguageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
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
            Title = "BookSubmit";
            if (ModelState.IsValid)
            {
                if(bookmodel.CoverPhoto != null && bookmodel.CoverPhoto.Length > 0)
                {
                    if(bookmodel.CoverPhoto.ContentType != "image/jpeg" && bookmodel.CoverPhoto.ContentType != "image/png")
                    {  
                        ModelState.AddModelError("CoverPhoto", "Only images with jpeg or png formats are allowed!");
                        return View("BookSubmit");
                    }
                    else
                    {
                        string folder = "books/cover/";
                      bookmodel.CoverImageUrl =  await UploadImage(folder, bookmodel.CoverPhoto);
                    }
                }
                if (bookmodel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";
                    bookmodel.Gallery = new List<GalleryModel>();
                    foreach (var file in bookmodel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.Name,
                            URL = await UploadImage(folder, file)
                        };
                        bookmodel.Gallery.Add(gallery);
                        
                    }
                }
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

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }
    }
}
