using System.Text.Json.Serialization;

namespace WebAppAPI.Models
{
    public class Company
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CompanyName { get; set; }

        public string CompanyLocalName { get; set; }

        public Guid? EmployeeId { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}
