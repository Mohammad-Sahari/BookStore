using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using BookStore.Models;
using Microsoft.Extensions.Configuration;
using BookStore.Services;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [ViewData]
        public BookModel Book { get; set; }

        [ViewData]
        public string Title { get; set; }
        public ViewResult index()
        {
            var userId = _userService.GetUserId();
            var isLoggedIn = _userService.IsAuthenticated();

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
