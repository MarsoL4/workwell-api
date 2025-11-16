using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Application.Services.EmpresaOrganizacao;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncionarioDto>>> GetAll()
        {
            var funcionarios = await _funcionarioService.GetAllAsync();
            return Ok(funcionarios);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<FuncionarioDto>> GetById(long id)
        {
            var funcionario = await _funcionarioService.GetByIdAsync(id);
            if (funcionario == null)
                return NotFound();
            return Ok(funcionario);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(FuncionarioDto funcionarioDto)
        {
            var id = await _funcionarioService.CreateAsync(funcionarioDto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, FuncionarioDto funcionarioDto)
        {
            if (id != funcionarioDto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _funcionarioService.UpdateAsync(funcionarioDto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _funcionarioService.DeleteAsync(id);
            return NoContent();
        }
    }
}