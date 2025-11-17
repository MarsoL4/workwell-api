using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.Indicadores;
using WorkWell.Application.Services.Indicadores;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class IndicadoresEmpresaController : ControllerBase
    {
        private readonly IIndicadoresEmpresaService _indicadoresService;

        public IndicadoresEmpresaController(IIndicadoresEmpresaService indicadoresService)
        {
            _indicadoresService = indicadoresService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndicadoresEmpresaDto>>> GetAll()
        {
            var list = await _indicadoresService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<IndicadoresEmpresaDto>> GetById(long id)
        {
            var entity = await _indicadoresService.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(IndicadoresEmpresaDto dto)
        {
            var id = await _indicadoresService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, IndicadoresEmpresaDto dto)
        {
            if (id != dto.Id) return BadRequest();
            await _indicadoresService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _indicadoresService.DeleteAsync(id);
            return NoContent();
        }
    }
}