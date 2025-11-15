using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Domain.Enums.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioController(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncionarioDto>>> GetAll()
        {
            var funcionarios = await _funcionarioRepository.GetAllAsync();
            var lista = funcionarios.Select(f => ToDto(f));
            return Ok(lista);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<FuncionarioDto>> GetById(long id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null)
                return NotFound();
            return Ok(ToDto(funcionario));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(FuncionarioDto funcionarioDto)
        {
            var funcionario = FromDto(funcionarioDto);
            await _funcionarioRepository.AddAsync(funcionario);
            return CreatedAtAction(nameof(GetById), new { id = funcionario.Id }, funcionario.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, FuncionarioDto funcionarioDto)
        {
            if (id != funcionarioDto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            var funcionario = FromDto(funcionarioDto);
            await _funcionarioRepository.UpdateAsync(funcionario);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _funcionarioRepository.DeleteAsync(id);
            return NoContent();
        }

        private static FuncionarioDto ToDto(WorkWell.Domain.Entities.EmpresaOrganizacao.Funcionario funcionario)
        {
            return new FuncionarioDto
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Cargo = funcionario.Cargo,
                Ativo = funcionario.Ativo,
                SetorId = funcionario.SetorId
            };
        }

        private static WorkWell.Domain.Entities.EmpresaOrganizacao.Funcionario FromDto(FuncionarioDto dto)
        {
            return new WorkWell.Domain.Entities.EmpresaOrganizacao.Funcionario
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Email = dto.Email,
                Cargo = dto.Cargo,
                Ativo = dto.Ativo,
                SetorId = dto.SetorId,
                Senha = string.Empty, // Ajuste conforme regras de negócio de senha
                TokenEmpresa = string.Empty // Ajuste conforme lógica de autenticação
            };
        }
    }
}