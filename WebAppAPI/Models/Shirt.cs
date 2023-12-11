using System.ComponentModel.DataAnnotations;
using WebAppAPI.Models.Validations;

namespace WebAppAPI.Models
{
    public class Shirt
    {
        public int ShirtID { get; set; }
        [Required]
        public string? Brand {  get; set; }
        public string Color { get; set; }
        [Shirt_EnsureCorrectSizing]
        public int? Size { get; set; }
        public string? MyProperty { get; set; }
        [Required]
        public string? Gender { get; set; }
        public double Price { get; set; }
    }
}
