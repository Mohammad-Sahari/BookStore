using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult index()
        {
            return View();
        }
        public ViewResult Aboutus()
        {
            return View();
        }
        public ViewResult ContactUs()
        {
            return View();
        }
        

    }
}
