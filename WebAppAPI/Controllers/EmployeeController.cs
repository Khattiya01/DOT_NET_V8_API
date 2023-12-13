using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Data;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
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
    }
}
