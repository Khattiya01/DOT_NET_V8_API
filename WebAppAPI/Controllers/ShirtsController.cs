using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI.Models;
using WebAppAPI.Models.Repositorys;

namespace WebAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController: ControllerBase
    {

        [HttpGet]
        public IActionResult GetAllShirts()
        {
            return Ok("Reading all the shirts");
        }
        [HttpGet("{id}")]
        public IActionResult GetShirts(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var shirt = ShirtRepository.GetShirtsById(id);
            if(shirt == null)
            {
                return NotFound(); // 404
            }
            return Ok(shirt); //200
        }
        [HttpPost]
        public IActionResult CreateShirts([FromBody] Shirt shirt)
        {
            return Ok("Creating shirt" + $" price : {shirt.Price}");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateShirts(int id)
        {
            return Ok($"Updating shirt with id: {id}");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteShirts(int id)
        {
            return Ok($"Deleting shirt with id: {id}");
        }
    }
}
