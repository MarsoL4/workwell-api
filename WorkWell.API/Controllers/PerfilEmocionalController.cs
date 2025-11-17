using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using WorkWell.Application.Services.AvaliacoesEmocionais;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PerfilEmocionalController : ControllerBase
    {
        private readonly IPerfilEmocionalService _perfilService;

        public PerfilEmocionalController(IPerfilEmocionalService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerfilEmocionalDto>>> GetAll()
        {
            var perfis = await _perfilService.GetAllAsync();
            return Ok(perfis);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<PerfilEmocionalDto>> GetById(long id)
        {
            var perfil = await _perfilService.GetByIdAsync(id);
            if (perfil == null)
                return NotFound();
            return Ok(perfil);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(PerfilEmocionalDto dto)
        {
            var id = await _perfilService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, PerfilEmocionalDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _perfilService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _perfilService.DeleteAsync(id);
            return NoContent();
        }
    }
}