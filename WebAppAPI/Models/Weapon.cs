using System.Text.Json.Serialization;

namespace WebAppAPI.Models
{
    public class Weapon
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string WeaponName { get; set; }

        public Guid? EmployeeId { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}
