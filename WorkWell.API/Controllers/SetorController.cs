using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetorController : ControllerBase
    {
        private readonly ISetorRepository _setorRepository;

        public SetorController(ISetorRepository setorRepository)
        {
            _setorRepository = setorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SetorDto>>> GetAll()
        {
            var setores = await _setorRepository.GetAllAsync();
            var lista = setores.Select(s => ToDto(s));
            return Ok(lista);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<SetorDto>> GetById(long id)
        {
            var setor = await _setorRepository.GetByIdAsync(id);
            if (setor == null)
                return NotFound();
            return Ok(ToDto(setor));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(SetorDto setorDto)
        {
            var setor = FromDto(setorDto);
            await _setorRepository.AddAsync(setor);
            return CreatedAtAction(nameof(GetById), new { id = setor.Id }, setor.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, SetorDto setorDto)
        {
            if (id != setorDto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            var setor = FromDto(setorDto);
            await _setorRepository.UpdateAsync(setor);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _setorRepository.DeleteAsync(id);
            return NoContent();
        }

        private static SetorDto ToDto(Setor setor)
        {
            return new SetorDto
            {
                Id = setor.Id,
                Nome = setor.Nome,
                EmpresaId = setor.EmpresaId
            };
        }

        private static Setor FromDto(SetorDto dto)
        {
            return new Setor
            {
                Id = dto.Id,
                Nome = dto.Nome,
                EmpresaId = dto.EmpresaId
            };
        }
    }
}