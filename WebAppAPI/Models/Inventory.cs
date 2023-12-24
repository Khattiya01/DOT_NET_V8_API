using System.Text.Json.Serialization;

namespace WebAppAPI.Models
{
    public class Inventory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<string>? Keys { get; set; }
        public List<int>? Values { get; set; }
        public Guid? SaveManagerId { get; set; }
        [JsonIgnore]
        public SaveManager? SaveManager { get; set; }
    }
}
