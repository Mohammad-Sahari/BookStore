using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using BookStore.Models;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {

        [ViewData]
        public BookModel Book { get; set; }

        [ViewData]
        public string Title { get; set; }
        public ViewResult index()
        {
            Title = "Home";
            Book = new BookModel(){Id = 1, Title = "PHP"};
            return View();
        }
        public ViewResult Aboutus()
        {
            Title = "About Us";
            return View();
        }
        public ViewResult ContactUs()
        {
            Title = "Contact Us";
            return View();
        }
        

    }
}
