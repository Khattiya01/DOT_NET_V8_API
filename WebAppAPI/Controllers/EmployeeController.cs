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
        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
            var datas = await _context.Employees.ToListAsync();
            return Ok( new
            {
                data = datas,
                meta = "meta"
            });
        }

        [HttpGet("id")]
        public async Task<ActionResult<List<Employee>>> GetEmployeeById(int id)
        {

            var employee = await _context.Employees.FindAsync(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return Ok(await _context.Employees.ToListAsync());

        }

    }
}
