using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Application.Services.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<EmpresaDto>>> GetAllPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _empresaService.GetAllPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<EmpresaDto>> GetById(long id)
        {
            var empresa = await _empresaService.GetByIdAsync(id);
            if (empresa == null)
                return NotFound();
            return Ok(empresa);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(EmpresaDto empresaDto)
        {
            var id = await _empresaService.CreateAsync(empresaDto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, EmpresaDto empresaDto)
        {
            if (id != empresaDto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _empresaService.UpdateAsync(empresaDto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _empresaService.DeleteAsync(id);
            return NoContent();
        }
    }
}