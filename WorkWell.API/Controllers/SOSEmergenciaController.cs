using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Application.Services.ApoioPsicologico;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SOSemergenciaController : ControllerBase
    {
        private readonly ISOSemergenciaService _sosService;

        public SOSemergenciaController(ISOSemergenciaService sosService)
        {
            _sosService = sosService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SOSemergenciaDto>>> GetAll()
        {
            var registros = await _sosService.GetAllAsync();
            return Ok(registros);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<SOSemergenciaDto>> GetById(long id)
        {
            var entidade = await _sosService.GetByIdAsync(id);
            if (entidade == null)
                return NotFound();
            return Ok(entidade);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(SOSemergenciaDto dto)
        {
            var id = await _sosService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, SOSemergenciaDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _sosService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _sosService.DeleteAsync(id);
            return NoContent();
        }
    }
}