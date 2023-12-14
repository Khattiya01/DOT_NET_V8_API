using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Data;
using WebAppAPI.Models.Repositorys;
using WebAppAPI.Models;
using WebAppAPI.Filters.ActionFilters;

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
        [Employee_ValidateCreateEmployeeFilter]
        public async Task<ActionResult<List<Employee>>> CreateEmployee([FromBody] Employee employee)
        {
                var existingEmployee = _context.Employees.FirstOrDefault(x =>
                    !string.IsNullOrWhiteSpace(employee.Firstname) &&
                    !string.IsNullOrWhiteSpace(x.Firstname) &&
                    x.Firstname.ToLower() == employee.Firstname.ToLower() &&
                    !string.IsNullOrWhiteSpace(employee.Lastname) &&
                    !string.IsNullOrWhiteSpace(x.Lastname) &&
                    x.Lastname.ToLower() == employee.Lastname.ToLower() &&
                    !string.IsNullOrWhiteSpace(employee.Phone) &&
                    !string.IsNullOrWhiteSpace(x.Phone) &&
                    x.Phone.ToLower() == employee.Phone.ToLower() &&
                    employee.Age.HasValue &&
                    x.Age.HasValue &&
                    employee.Age.Value == x.Age.Value);

                if (existingEmployee != null)
                {
                    return BadRequest("Employee already exists...");
                } else
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
           


        }

        [HttpPut("id")]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee([FromBody] Employee employee)
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
