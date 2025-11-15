using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Application.Services.EmpresaOrganizacao;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaDto>>> GetAll()
        {
            var empresas = await _empresaService.GetAllAsync();
            return Ok(empresas);
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