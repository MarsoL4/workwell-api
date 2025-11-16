using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Application.Services.ApoioPsicologico;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaPsicologicaController : ControllerBase
    {
        private readonly IConsultaPsicologicaService _consultaService;

        public ConsultaPsicologicaController(IConsultaPsicologicaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultaPsicologicaDto>>> GetAll()
        {
            var consultas = await _consultaService.GetAllAsync();
            return Ok(consultas);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ConsultaPsicologicaDto>> GetById(long id)
        {
            var consulta = await _consultaService.GetByIdAsync(id);
            if (consulta == null)
                return NotFound();
            return Ok(consulta);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(ConsultaPsicologicaDto dto)
        {
            var id = await _consultaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, ConsultaPsicologicaDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _consultaService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _consultaService.DeleteAsync(id);
            return NoContent();
        }
    }
}