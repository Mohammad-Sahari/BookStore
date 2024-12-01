using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using BookStore.Models;
using Microsoft.Extensions.Configuration;
using BookStore.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public HomeController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [ViewData]
        public BookModel Book { get; set; }

        [ViewData]
        public string Title { get; set; }
        public async Task<IActionResult> index()
        {
            //UserEmailOptions options = new UserEmailOptions()
            //{
            //    ToEmails = new List<string>() { "test@gmail.com" },
            //    PlaceHolders= new List<KeyValuePair<string, string>>
            //    {
            //        new KeyValuePair<string, string>("{{UserName}}", "Mohammad")
            //    }
            //};

            //await _emailService.SendTestEmail(options);

            var userId = _userService.GetUserId();
            var isLoggedIn = _userService.IsAuthenticated();

            Title = "Home";
            //Book = new BookModel(){Id = 1, Title = "PHP"};
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
