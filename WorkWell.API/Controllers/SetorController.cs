using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Application.Services.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SetorController : ControllerBase
    {
        private readonly ISetorService _setorService;

        public SetorController(ISetorService setorService)
        {
            _setorService = setorService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<SetorDto>>> GetAllPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _setorService.GetAllPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<SetorDto>> GetById(long id)
        {
            var setor = await _setorService.GetByIdAsync(id);
            if (setor == null)
                return NotFound();
            return Ok(setor);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(SetorDto setorDto)
        {
            var id = await _setorService.CreateAsync(setorDto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, SetorDto setorDto)
        {
            if (id != setorDto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _setorService.UpdateAsync(setorDto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _setorService.DeleteAsync(id);
            return NoContent();
        }
    }
}