using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Application.Services.ApoioPsicologico;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PsicologoController : ControllerBase
    {
        private readonly IPsicologoService _psicologoService;

        public PsicologoController(IPsicologoService psicologoService)
        {
            _psicologoService = psicologoService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<PsicologoDto>>> GetAllPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _psicologoService.GetAllPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<PsicologoDto>> GetById(long id)
        {
            var psicologo = await _psicologoService.GetByIdAsync(id);
            if (psicologo == null)
                return NotFound();
            return Ok(psicologo);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(PsicologoDto dto)
        {
            var id = await _psicologoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, PsicologoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _psicologoService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _psicologoService.DeleteAsync(id);
            return NoContent();
        }
    }
}