using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebAppAPI.Models.Validations;

namespace WebAppAPI.Models
{
    public class SaveManager
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? Currency { get; set; }
        public SkillTree? SkillTree { get; set; }
        public Inventory? Inventory { get; set; }
        public List<string>? EquipmentId { get; set; }
        public CheckPoints? Checkpoints { get; set; }
        public string? closestCheckpointId { get; set; }
        public double? lostcurrencyX { get; set; }
        public double? lostcurrencyY { get; set; }
        public int? lostCurrencyAmount { get; set; }
        public VolumeSettings? VolumeSettings { get; set; }
        public Guid? EmployeeId { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }
    }
}

