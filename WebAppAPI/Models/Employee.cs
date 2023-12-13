using System.ComponentModel.DataAnnotations;
using WebAppAPI.Models.Validations;

namespace WebAppAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public int? Age { get; set; }
        public string? Country { get; set; }
        [Required]
        [Employee_EnsureCorrectPhone]
        public int Phone { get; set; }
    }
}
