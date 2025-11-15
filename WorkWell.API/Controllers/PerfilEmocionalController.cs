using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilEmocionalController : ControllerBase
    {
        private readonly IPerfilEmocionalRepository _repository;

        public PerfilEmocionalController(IPerfilEmocionalRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilEmocionalDto>>> GetAll()
        {
            var perfis = await _repository.GetAllAsync();
            return Ok(perfis.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<PerfilEmocionalDto>> GetById(long id)
        {
            var perfil = await _repository.GetByIdAsync(id);
            if (perfil == null)
                return NotFound();
            return Ok(ToDto(perfil));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(PerfilEmocionalDto dto)
        {
            var perfil = FromDto(dto);
            await _repository.AddAsync(perfil);
            return CreatedAtAction(nameof(GetById), new { id = perfil.Id }, perfil.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, PerfilEmocionalDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            var perfil = FromDto(dto);
            await _repository.UpdateAsync(perfil);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private static PerfilEmocionalDto ToDto(PerfilEmocional entidade) =>
            new()
            {
                Id = entidade.Id,
                FuncionarioId = entidade.FuncionarioId,
                HumorInicial = entidade.HumorInicial,
                Rotina = entidade.Rotina,
                PrincipaisEstressores = entidade.PrincipaisEstressores,
                DataCriacao = entidade.DataCriacao
            };

        private static PerfilEmocional FromDto(PerfilEmocionalDto dto) =>
            new()
            {
                Id = dto.Id,
                FuncionarioId = dto.FuncionarioId,
                HumorInicial = dto.HumorInicial,
                Rotina = dto.Rotina,
                PrincipaisEstressores = dto.PrincipaisEstressores,
                DataCriacao = dto.DataCriacao
            };
    }
}