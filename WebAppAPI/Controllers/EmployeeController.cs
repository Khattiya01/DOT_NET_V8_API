using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Data;
using WebAppAPI.Models.Repositorys;
using WebAppAPI.Models;

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
        public async Task<ActionResult> GetAllEmployee()
        {
            var datas = await _context.Shirt.ToListAsync();
            return Ok(datas);
            /* return Ok(new { data = ShirtRepository.GetShirts(),
             meta = "meta"
             });*/
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetEmployeeById(int id)
        {

            var employee = await _context.Employees.FindAsync(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _context.Employees.Add(employee);
            return Ok();
        }
    }
}
