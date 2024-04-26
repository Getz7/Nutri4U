using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MenuCliente()
        {
            return View();
        }
        public IActionResult PerfilCliente()
        {
            return View();
        }
        public IActionResult UpdateUserCliente()
        {
            return View();
        }
        public IActionResult ViewUserFood()
        {
            return View();
        }
        public IActionResult ViewFoodClient()
        {
            return View();
        }
        public IActionResult ViewSpecificFoodClient()
        {
            return View();
        }
    }
}
