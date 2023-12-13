using Microsoft.AspNetCore.Mvc;
using WebAppAPI.Filters.ActionFilters;
using WebAppAPI.Filters.ExceptionFilters;
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
            return Ok(new { data = ShirtRepository.GetShirts(),
            meta = "meta"
            });
        }
        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult GetShirts(int id)
        {
            return Ok(ShirtRepository.GetShirtsById(id)); //200
        }
        [HttpPost]
        [Shirt_ValidateCreateShirtFilter]
        public IActionResult CreateShirts([FromBody] Shirt shirt)
        {
            ShirtRepository.AddShirt(shirt);
            return CreatedAtAction(nameof(GetShirts), new { id = shirt.ShirtID }, shirt);
        }
        [HttpPut("{id}")]
        [Shirt_ValidateShirtIdFilter]
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_HandleUpdateExceptionFilter]
        public IActionResult UpdateShirts(int id, Shirt shirt)
        {
            ShirtRepository.UpdateShirt(shirt);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult DeleteShirts(int id)
        {
            var shirt = ShirtRepository.GetShirtsById(id);
            ShirtRepository.DeleteShirt(id);
            return Ok(shirt);
        }
    }
}
