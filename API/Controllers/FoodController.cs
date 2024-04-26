using AppLogic;
using CoreApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modelo;

namespace WebAPI.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        ///Controlador de mantenimiento del usuario.
        ///C -> Create
        ///R -> Retrieve
        ///U -> Update
        ///D -> Delete

        [HttpPost]
       
        public async Task<IActionResult> Create(Food food)
        {
            var um = new FoodManager();

            try
            {
                um.Create(food);
                return Ok(food);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]

        public async Task<IActionResult> Update(Food food)
        {
            var um = new FoodManager();

            try
            {
                um.Update(food);
                return Ok(food);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]

        public async Task<IActionResult> Delete(string id)
        {
            var um = new FoodManager();

            try
            {
                um.Delete(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]

        public async Task<IActionResult> RetrieveAll()
        {
            try
            {
                var um = new FoodManager();

                return Ok(await um.RetrieveAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet]

        public async Task<IActionResult> GetFoodById(string id)
        {
            try
            {
                var um = new FoodManager();

                return Ok(await um.GetFoodById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
