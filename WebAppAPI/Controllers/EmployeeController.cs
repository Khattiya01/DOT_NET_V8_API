using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Data;
using WebAppAPI.Models;
using WebAppAPI.Filters.ActionFilters;
using WebAppAPI.Filters.ExceptionFilters;

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

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
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
        [ServiceFilter(typeof(Employee_ValidateEmployeeIdIdFilterAttribute))]
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
        [ServiceFilter(typeof(Employee_ValidateCreateEmployeeFilterAttribute))]
        public async Task<ActionResult<List<Employee>>> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChangesAsync();
                return Ok(new { employee });
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [HttpPut("id")]
        [ServiceFilter(typeof(Employee_ValidateEmployeeIdIdFilterAttribute))]
        [Employee_ValidateUpdateEmployeeFilterAtteibute]
        [Employee_HandleUpdateExceptionFilter]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                var dbEmployee = _context.Employees.First(x => x.Id == employee.Id);
                if(dbEmployee is null) return NotFound("Employee not found.");

                dbEmployee.Firstname = employee.Firstname;
                dbEmployee.Lastname = employee.Lastname;
                dbEmployee.Phone = employee.Phone;
                dbEmployee.Country = employee.Country;
                dbEmployee.Age = employee.Age;

                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest();
            }

            return Ok(new { employee });

        }

        [HttpDelete("id")]
        [ServiceFilter(typeof(Employee_ValidateEmployeeIdIdFilterAttribute))]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            try
            {
                var dbEmployee = _context.Employees.First(x => x.Id == id);
                if (dbEmployee is null) return NotFound("Employee not found.");

                _context.Employees.Remove(dbEmployee);
                _context.SaveChangesAsync();

                return Ok();
            } 
            catch { return BadRequest(); }
        }

    }
}
