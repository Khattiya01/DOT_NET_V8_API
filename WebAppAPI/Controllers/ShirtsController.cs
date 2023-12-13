using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Data;
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
        private readonly DataContext _context;

        public ShirtsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllShirts()
        {
            var datas = await _context.Shirt.ToListAsync();
            return Ok(datas);
           /* return Ok(new { data = ShirtRepository.GetShirts(),
            meta = "meta"
            });*/
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
