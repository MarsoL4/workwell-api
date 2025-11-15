using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaPsicologicaController : ControllerBase
    {
        private readonly IConsultaPsicologicaRepository _repository;

        public ConsultaPsicologicaController(IConsultaPsicologicaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultaPsicologicaDto>>> GetAll()
        {
            var consultas = await _repository.GetAllAsync();
            return Ok(consultas.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ConsultaPsicologicaDto>> GetById(long id)
        {
            var consulta = await _repository.GetByIdAsync(id);
            if (consulta == null)
                return NotFound();
            return Ok(ToDto(consulta));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(ConsultaPsicologicaDto dto)
        {
            var consulta = FromDto(dto);
            await _repository.AddAsync(consulta);
            return CreatedAtAction(nameof(GetById), new { id = consulta.Id }, consulta.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, ConsultaPsicologicaDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            var consulta = FromDto(dto);
            await _repository.UpdateAsync(consulta);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private static ConsultaPsicologicaDto ToDto(ConsultaPsicologica entidade) =>
            new()
            {
                Id = entidade.Id,
                FuncionarioId = entidade.FuncionarioId,
                PsicologoId = entidade.PsicologoId,
                DataConsulta = entidade.DataConsulta,
                Tipo = entidade.Tipo,
                Status = entidade.Status,
                AnotacoesSigilosas = entidade.AnotacoesSigilosas
            };

        private static ConsultaPsicologica FromDto(ConsultaPsicologicaDto dto) =>
            new()
            {
                Id = dto.Id,
                FuncionarioId = dto.FuncionarioId,
                PsicologoId = dto.PsicologoId,
                DataConsulta = dto.DataConsulta,
                Tipo = dto.Tipo,
                Status = dto.Status,
                AnotacoesSigilosas = dto.AnotacoesSigilosas
            };
    }
}