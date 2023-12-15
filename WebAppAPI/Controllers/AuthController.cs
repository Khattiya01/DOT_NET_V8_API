using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Data;
using WebAppAPI.Models;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<List<User>>> Login(User request)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Email == request.Email);
            if (employee == null)
            {
                return BadRequest("Employee not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, employee.Password))
            {
                return BadRequest("Wrong password");
            }

            return Ok(employee);
        }
    }
}
