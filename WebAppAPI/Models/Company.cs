using System.Text.Json.Serialization;

namespace WebAppAPI.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public string CompanyLocalName { get; set; }

        public int? EmployeeId { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}
