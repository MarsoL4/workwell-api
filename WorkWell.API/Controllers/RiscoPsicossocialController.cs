using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Application.Services.AvaliacoesEmocionais;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RiscoPsicossocialController : ControllerBase
    {
        private readonly IRiscoPsicossocialService _riscoService;

        public RiscoPsicossocialController(IRiscoPsicossocialService riscoService)
        {
            _riscoService = riscoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RiscoPsicossocialDto>>> GetAll()
        {
            var registros = await _riscoService.GetAllAsync();
            return Ok(registros);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<RiscoPsicossocialDto>> GetById(long id)
        {
            var entity = await _riscoService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(RiscoPsicossocialDto dto)
        {
            var id = await _riscoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, RiscoPsicossocialDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _riscoService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _riscoService.DeleteAsync(id);
            return NoContent();
        }
    }
}