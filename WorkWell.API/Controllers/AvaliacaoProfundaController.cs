using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoProfundaController : ControllerBase
    {
        private readonly IAvaliacaoProfundaRepository _repository;

        public AvaliacaoProfundaController(IAvaliacaoProfundaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvaliacaoProfundaDto>>> GetAll()
        {
            var registros = await _repository.GetAllAsync();
            return Ok(registros.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<AvaliacaoProfundaDto>> GetById(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return Ok(ToDto(entity));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(AvaliacaoProfundaDto dto)
        {
            var entity = FromDto(dto);
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, AvaliacaoProfundaDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            var entity = FromDto(dto);
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private static AvaliacaoProfundaDto ToDto(AvaliacaoProfunda entidade) =>
            new()
            {
                Id = entidade.Id,
                FuncionarioId = entidade.FuncionarioId,
                Gad7Score = entidade.Gad7Score,
                Phq9Score = entidade.Phq9Score,
                Interpretacao = entidade.Interpretacao,
                DataRegistro = entidade.DataRegistro
            };

        private static AvaliacaoProfunda FromDto(AvaliacaoProfundaDto dto) =>
            new()
            {
                Id = dto.Id,
                FuncionarioId = dto.FuncionarioId,
                Gad7Score = dto.Gad7Score,
                Phq9Score = dto.Phq9Score,
                Interpretacao = dto.Interpretacao,
                DataRegistro = dto.DataRegistro
            };
    }
}