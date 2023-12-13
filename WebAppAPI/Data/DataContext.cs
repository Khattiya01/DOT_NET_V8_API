using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Models;

namespace WebAppAPI.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { 

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shirt> Shirts { get; set; }
    }
}
