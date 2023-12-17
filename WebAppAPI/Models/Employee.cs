﻿using System.ComponentModel.DataAnnotations;
using WebAppAPI.Models.Validations;

namespace WebAppAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public Guid UserId { get; set; } = Guid.NewGuid();
        public DateTime CreateAt { get; set; } = DateTime.Now;
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public int? Age { get; set; }
        public string? Country { get; set; }
        [Required]
        [Employee_EnsureCorrectPhone]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public List<string> Role { get; set; }
    }
}
