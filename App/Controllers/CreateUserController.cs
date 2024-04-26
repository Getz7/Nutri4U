using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class CreateUserController : Controller
    {
        public IActionResult CreateUser()
        {
            return View();
        }
    }
}
