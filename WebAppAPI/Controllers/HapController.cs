using Microsoft.AspNetCore.Mvc;

namespace WebAppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HapController: ControllerBase
    {
        [HttpGet]
        public string GetAllHap() {
            return "All Hap";
        }
        [HttpGet("{id}")]
        public string GetHap(int id)
        {
            return $"Hap {id}";
        }
        [HttpPost]
        public string CreateHap() {
            return "Creating Hap";
        }
        [HttpPut("{id}")]
        public string UpdateHap(int id)
        {
            return $"update Hap {id}";
        }
        [HttpDelete("{id}")]
        public string DeleteHap(int id)
        {
            return $"delete Hap {id}";
        }
    }
}
