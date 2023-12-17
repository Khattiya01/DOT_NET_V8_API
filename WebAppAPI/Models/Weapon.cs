using System.Text.Json.Serialization;

namespace WebAppAPI.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string WeaponName { get; set; }

        public int? EmployeeId { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}
