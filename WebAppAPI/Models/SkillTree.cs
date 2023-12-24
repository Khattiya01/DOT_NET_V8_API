using System.Text.Json.Serialization;

namespace WebAppAPI.Models
{
    public class SkillTree
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<string>? Keys { get; set; }
        public List<bool>? Values { get; set; }
        public Guid? SaveManagerId { get; set; }
        [JsonIgnore]
        public SaveManager? SaveManager { get; set; }

    }
}
