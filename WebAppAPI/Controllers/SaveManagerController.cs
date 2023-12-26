using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Data;
using WebAppAPI.Models;

namespace WebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveManagerController : ControllerBase
    {
        private readonly DataContext _context;
        private static SaveManager _saveManager = new SaveManager();

        private readonly ILogger<EmployeeController> _logger;

        public SaveManagerController(ILogger<EmployeeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<SaveManager>>> GetAllSaveManager()
        {
            var datas = await _context.SaveManagers.Include(s=> s.Inventory).Include(s => s.SkillTree).Include(s => s.Checkpoints).Include(s => s.VolumeSettings).ToListAsync();

            return Ok(new
            {
                data = datas,
                meta = "meta"
            });
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Employee>> GetSaveManagerById(Guid id)
        {
            var saveManager = await _context.SaveManagers.Include(s => s.Inventory).Include(s => s.SkillTree).Include(s => s.Checkpoints).Include(s => s.VolumeSettings).FirstOrDefaultAsync(x => x.Id == id);
            if (saveManager is null)
            {
                return NotFound();
            }
            return Ok(saveManager);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<List<SaveManager>>> CreateSaveManager([FromBody] SaveManager saveManager)
        {
            try
            {
                _saveManager = saveManager;
                _saveManager.CreatedAt = DateTime.UtcNow;
                _saveManager.UpdatedAt = DateTime.UtcNow;
                _saveManager.SkillTree = saveManager.SkillTree;
                _context.SaveManagers.Add(_saveManager);
                await _context.SaveChangesAsync();
                return Ok(new { _saveManager });
            }
            catch
            {
                return BadRequest("error save failed");
            }
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<List<SaveManager>>> UpdateSaveManager(Guid id, [FromBody] SaveManager saveManager)
        {
            try
            {
                var dbSavemanager = await _context.SaveManagers.Include(sm => sm.Inventory).Include(sm=> sm.SkillTree).Include(sm => sm.VolumeSettings).Include(sm => sm.Checkpoints).FirstAsync(x => x.Id == saveManager.Id);

                if (dbSavemanager.Inventory is not null)
                {
                    var dbInventory = await _context.Inventorys.FirstAsync(iv => dbSavemanager.Inventory.Id == iv.Id);
                    dbInventory.Keys = saveManager.Inventory!.Keys;
                    dbInventory.Values = saveManager.Inventory!.Values;
                }
                if (dbSavemanager.SkillTree is not null)
                {
                    var dbSkillTree = await _context.SkillTrees.FirstAsync(iv => dbSavemanager.SkillTree.Id == iv.Id);
                    dbSkillTree.Keys = saveManager.SkillTree!.Keys;
                    dbSkillTree.Values = saveManager.SkillTree!.Values;
                }
                if (dbSavemanager.VolumeSettings is not null)
                {
                    var dbVolumeSetting = await _context.VolumeSettings.FirstAsync(iv => dbSavemanager.VolumeSettings.Id == iv.Id);
                    dbVolumeSetting.Keys = saveManager.VolumeSettings!.Keys;
                    dbVolumeSetting.Values = saveManager.VolumeSettings!.Values;
                }
                if (dbSavemanager.Checkpoints is not null)
                {
                    var dbCheckpoints = await _context.CheckPoints.FirstAsync(iv => dbSavemanager.Checkpoints.Id == iv.Id);
                    dbCheckpoints.Keys = saveManager.Checkpoints!.Keys;
                    dbCheckpoints.Values = saveManager.Checkpoints!.Values;
                }

                if (dbSavemanager is null) return NotFound("File Save not found.");

                dbSavemanager.UpdatedAt = DateTime.UtcNow;
                dbSavemanager.Currency = saveManager.Currency;
                dbSavemanager.EquipmentId = saveManager.EquipmentId;
                dbSavemanager.closestCheckpointId = saveManager.closestCheckpointId;
                dbSavemanager.lostcurrencyY = saveManager.lostcurrencyY;
                dbSavemanager.lostcurrencyX = saveManager.lostcurrencyX;
                dbSavemanager.lostCurrencyAmount = saveManager.lostCurrencyAmount;

                await _context.SaveChangesAsync();
                return Ok(new { dbSavemanager });
            }
            catch
            {
                return BadRequest();
            }

        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<List<SaveManager>>> DeleteSaveManager(Guid id)
        {
            try
            {
                var dbSaveManager = await _context.SaveManagers.FirstAsync(x => x.Id == id);
                if (dbSaveManager is null) return NotFound("File Save not found.");

                _context.SaveManagers.Remove(dbSaveManager);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch { return BadRequest(); }
        }
    }
}
