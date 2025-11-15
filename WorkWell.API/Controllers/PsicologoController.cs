using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PsicologoController : ControllerBase
    {
        private readonly IPsicologoRepository _repository;

        public PsicologoController(IPsicologoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PsicologoDto>>> GetAll()
        {
            var psicologos = await _repository.GetAllAsync();
            return Ok(psicologos.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<PsicologoDto>> GetById(long id)
        {
            var psicologo = await _repository.GetByIdAsync(id);
            if (psicologo == null)
                return NotFound();
            return Ok(ToDto(psicologo));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(PsicologoDto dto)
        {
            var psicologo = FromDto(dto);
            await _repository.AddAsync(psicologo);
            return CreatedAtAction(nameof(GetById), new { id = psicologo.Id }, psicologo.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, PsicologoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            var psicologo = FromDto(dto);
            await _repository.UpdateAsync(psicologo);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private static PsicologoDto ToDto(Psicologo entidade) =>
            new()
            {
                Id = entidade.Id,
                Nome = entidade.Nome,
                Email = entidade.Email,
                Crp = entidade.Crp,
                Ativo = entidade.Ativo,
                SetorId = entidade.SetorId
            };

        private static Psicologo FromDto(PsicologoDto dto) =>
            new()
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Email = dto.Email,
                Crp = dto.Crp,
                Ativo = dto.Ativo,
                SetorId = dto.SetorId
            };
    }
}