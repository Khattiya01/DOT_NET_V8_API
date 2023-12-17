using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Data;
using WebAppAPI.Models;
using WebAppAPI.Filters.ActionFilters;
using WebAppAPI.Filters.ExceptionFilters;
using Microsoft.AspNetCore.Authorization;
using WebAppAPI.Identity;
using System.Security.Claims;
using System.Data;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        private static Employee _employee = new Employee();

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
            var datas = await _context.Employees.Include(e=> e.company).Include(e=> e.weapons).ToListAsync();

            return Ok( new
            {
                data = datas,
                meta = "meta"
            });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("{id:guid}")]
        [ServiceFilter(typeof(Employee_ValidateEmployeeIdIdFilterAttribute))]
        public async Task<ActionResult<Employee>> GetEmployeeById(Guid id)
        {

            var employee = await _context.Employees.Include(e => e.company).Include(e => e.weapons).FirstOrDefaultAsync(x => x.UserId == id);
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
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(employee.Password);

                _employee = employee;
                _employee.UserId = Guid.NewGuid();
                _employee.CreatedAt = DateTime.UtcNow;
                _employee.UpdatedAt = DateTime.UtcNow;
                _employee.Password = passwordHash;
                _context.Employees.Add(_employee);
                await _context.SaveChangesAsync();
                return Ok(new { _employee });
            }
            catch
            {
                return BadRequest("error create employee failed");
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(Employee_ValidateEmployeeIdIdFilterAttribute))]
        [Employee_ValidateUpdateEmployeeFilterAtteibute]
        [Employee_HandleUpdateExceptionFilter]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Guid id, [FromBody] Employee employee)
        {
            try
            {
                var dbEmployee = _context.Employees.First(x => x.UserId == employee.UserId);
                if(dbEmployee is null) return NotFound("Employee not found.");

                dbEmployee.Email = employee.Email;
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

        [Authorize(Policy = "AdminPolicy")]
        /*        [RequiresClaim(IdentityData.AdminUserClaimName, "true")]*/
        [HttpDelete("{id:guid}")]
        [ServiceFilter(typeof(Employee_ValidateEmployeeIdIdFilterAttribute))]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(Guid id)
        {
            try
            {
                var dbEmployee = _context.Employees.First(x => x.UserId == id);
                if (dbEmployee is null) return NotFound("Employee not found.");

                _context.Employees.Remove(dbEmployee);
                _context.SaveChangesAsync();

                return Ok();
            } 
            catch { return BadRequest(); }
        }

    }
}
