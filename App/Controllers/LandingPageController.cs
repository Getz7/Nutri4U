using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class LandingPageController : Controller
    {
        public IActionResult LandingPage()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View("~/Views/Client/Login.cshtml");
        }
        public IActionResult CreateUser()
        {
            return View("~/Views/Client/CreateUser.cshtml");
        }
    }
}
