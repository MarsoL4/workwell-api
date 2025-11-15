using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiscoPsicossocialController : ControllerBase
    {
        private readonly IRiscoPsicossocialRepository _repository;

        public RiscoPsicossocialController(IRiscoPsicossocialRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RiscoPsicossocialDto>>> GetAll()
        {
            var registros = await _repository.GetAllAsync();
            return Ok(registros.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<RiscoPsicossocialDto>> GetById(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            return Ok(ToDto(entity));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(RiscoPsicossocialDto dto)
        {
            var entity = FromDto(dto);
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, RiscoPsicossocialDto dto)
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

        private static RiscoPsicossocialDto ToDto(RiscoPsicossocial entidade) =>
            new()
            {
                Id = entidade.Id,
                FuncionarioId = entidade.FuncionarioId,
                Categoria = entidade.Categoria,
                NivelRisco = entidade.NivelRisco,
                DataRegistro = entidade.DataRegistro
            };

        private static RiscoPsicossocial FromDto(RiscoPsicossocialDto dto) =>
            new()
            {
                Id = dto.Id,
                FuncionarioId = dto.FuncionarioId,
                Categoria = dto.Categoria,
                NivelRisco = dto.NivelRisco,
                DataRegistro = dto.DataRegistro
            };
    }
}