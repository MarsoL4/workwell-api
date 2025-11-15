using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SOSemergenciaController : ControllerBase
    {
        private readonly ISOSemergenciaRepository _repository;

        public SOSemergenciaController(ISOSemergenciaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SOSemergenciaDto>>> GetAll()
        {
            var registros = await _repository.GetAllAsync();
            return Ok(registros.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<SOSemergenciaDto>> GetById(long id)
        {
            var entidade = await _repository.GetByIdAsync(id);
            if (entidade == null)
                return NotFound();
            return Ok(ToDto(entidade));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(SOSemergenciaDto dto)
        {
            var entidade = FromDto(dto);
            await _repository.AddAsync(entidade);
            return CreatedAtAction(nameof(GetById), new { id = entidade.Id }, entidade.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, SOSemergenciaDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            var entidade = FromDto(dto);
            await _repository.UpdateAsync(entidade);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private static SOSemergenciaDto ToDto(SOSemergencia entidade) =>
            new()
            {
                Id = entidade.Id,
                FuncionarioId = entidade.FuncionarioId,
                DataAcionamento = entidade.DataAcionamento,
                Tipo = entidade.Tipo,
                PsicologoNotificado = entidade.PsicologoNotificado
            };

        private static SOSemergencia FromDto(SOSemergenciaDto dto) =>
            new()
            {
                Id = dto.Id,
                FuncionarioId = dto.FuncionarioId,
                DataAcionamento = dto.DataAcionamento,
                Tipo = dto.Tipo,
                PsicologoNotificado = dto.PsicologoNotificado
            };
    }
}