using System.Text.Json.Serialization;

namespace WebAppAPI.Models
{
    public class VolumeSettings
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<string>? Keys { get; set; }
        public List<double>? Values { get; set; }
        public Guid? SaveManagerId { get; set; }
        [JsonIgnore]
        public SaveManager? SaveManager { get; set; }
    }
}
