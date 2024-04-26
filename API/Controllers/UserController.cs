using AppLogic;
using CoreApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using Models;

namespace WebAPI.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ///Controlador de mantenimiento del usuario.
        ///C -> Create
        ///R -> Retrieve
        ///U -> Update
        ///D -> Delete

        [HttpPost]

        public async Task<IActionResult> Create(User user)
        {
            var um = new UserManager();

            try
            {
                um.Create(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]

        public async Task<IActionResult> Update(User user)
        {
            var um = new UserManager();

            try
            {
                um.Update(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveAll")]
        public async Task<IActionResult> RetrieveAll()
        {
            try
            {
                var um = new FoodManager();

                return Ok(um.RetrieveAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string password)
        {
            var userManager = new UserManager();

            try
            {
                var user = await userManager.LoginUser(correo, password);
                if (user != null)
                {
                    // User logged in successfully
                    return Ok(user);
                }
                else
                {
                    // Invalid credentials
                    return BadRequest("Invalid correo or password");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]

        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var um = new UserManager();

                return Ok(await um.GetUserById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]

        public async Task<IActionResult> GetUserFoodById(string id)
        {
            try
            {
                var um = new UserManager();

                return Ok(await um.GetUserFoodById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> DeleteUserFood(string userId, string idfood)
        {
            var um = new UserManager();

            try
            {
                um.Delete(userId, idfood);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserFood(UserFood food)
        {
            var um = new UserManager();

            try
            {
                um.CreateUserFood(food);
                return Ok(food);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
