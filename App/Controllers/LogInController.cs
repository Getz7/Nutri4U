using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
