using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodCheckController : ControllerBase
    {
        private readonly IMoodCheckRepository _repository;

        public MoodCheckController(IMoodCheckRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoodCheckDto>>> GetAll()
        {
            var registros = await _repository.GetAllAsync();
            return Ok(registros.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<MoodCheckDto>> GetById(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return Ok(ToDto(entity));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(MoodCheckDto dto)
        {
            var entity = FromDto(dto);
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, MoodCheckDto dto)
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

        private static MoodCheckDto ToDto(MoodCheck entidade) =>
            new()
            {
                Id = entidade.Id,
                FuncionarioId = entidade.FuncionarioId,
                Humor = entidade.Humor,
                Produtivo = entidade.Produtivo,
                Estressado = entidade.Estressado,
                DormiuBem = entidade.DormiuBem,
                DataRegistro = entidade.DataRegistro
            };

        private static MoodCheck FromDto(MoodCheckDto dto) =>
            new()
            {
                Id = dto.Id,
                FuncionarioId = dto.FuncionarioId,
                Humor = dto.Humor,
                Produtivo = dto.Produtivo,
                Estressado = dto.Estressado,
                DormiuBem = dto.DormiuBem,
                DataRegistro = dto.DataRegistro
            };
    }
}