using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        private readonly IConfiguration _configuration;

        private readonly JwtService _jwtService;

        public AuthController(ILogger<AuthController> logger, DataContext context, IConfiguration  configuration, JwtService jwtService)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _jwtService = jwtService;
            _jwtService = jwtService;
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

            /*   string token = CreateToken(employee);*/

            var token = _jwtService.GenerateToken(employee.Id, employee.Email, employee.Role);

   
            return Ok(new { token = token, employee = employee });
        }

        private string CreateToken(Employee user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtSettings:Key").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
