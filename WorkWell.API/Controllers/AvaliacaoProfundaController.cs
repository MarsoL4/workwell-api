using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Application.Services.AvaliacoesEmocionais;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AvaliacaoProfundaController : ControllerBase
    {
        private readonly IAvaliacaoProfundaService _avaliacaoService;

        public AvaliacaoProfundaController(IAvaliacaoProfundaService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvaliacaoProfundaDto>>> GetAll()
        {
            var registros = await _avaliacaoService.GetAllAsync();
            return Ok(registros);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<AvaliacaoProfundaDto>> GetById(long id)
        {
            var entity = await _avaliacaoService.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(AvaliacaoProfundaDto dto)
        {
            var id = await _avaliacaoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, AvaliacaoProfundaDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _avaliacaoService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _avaliacaoService.DeleteAsync(id);
            return NoContent();
        }
    }
}