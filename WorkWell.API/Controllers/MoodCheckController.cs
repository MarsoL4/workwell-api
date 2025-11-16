using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Application.Services.AvaliacoesEmocionais;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodCheckController : ControllerBase
    {
        private readonly IMoodCheckService _moodService;

        public MoodCheckController(IMoodCheckService moodService)
        {
            _moodService = moodService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoodCheckDto>>> GetAll()
        {
            var registros = await _moodService.GetAllAsync();
            return Ok(registros);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<MoodCheckDto>> GetById(long id)
        {
            var entity = await _moodService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(MoodCheckDto dto)
        {
            var id = await _moodService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, MoodCheckDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _moodService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _moodService.DeleteAsync(id);
            return NoContent();
        }
    }
}