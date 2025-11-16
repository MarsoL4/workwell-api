using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;

namespace WorkWell.Application.Services.EmpresaOrganizacao
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<IEnumerable<FuncionarioDto>> GetAllAsync()
        {
            var funcionarios = await _funcionarioRepository.GetAllAsync();
            var dtos = new List<FuncionarioDto>();
            foreach (var funcionario in funcionarios)
            {
                dtos.Add(ToDto(funcionario));
            }
            return dtos;
        }

        public async Task<FuncionarioDto?> GetByIdAsync(long id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            return funcionario == null ? null : ToDto(funcionario);
        }

        public async Task<long> CreateAsync(FuncionarioDto funcionarioDto)
        {
            var funcionario = FromDto(funcionarioDto);
            await _funcionarioRepository.AddAsync(funcionario);
            return funcionario.Id;
        }

        public async Task UpdateAsync(FuncionarioDto funcionarioDto)
        {
            var funcionario = FromDto(funcionarioDto);
            await _funcionarioRepository.UpdateAsync(funcionario);
        }

        public async Task DeleteAsync(long id)
        {
            await _funcionarioRepository.DeleteAsync(id);
        }

        private static FuncionarioDto ToDto(Funcionario funcionario)
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

        private static Funcionario FromDto(FuncionarioDto dto)
        {
            return new Funcionario
            {
                Id = dto.Id,
                Nome = dto.Nome,
                Email = dto.Email,
                Cargo = dto.Cargo,
                Ativo = dto.Ativo,
                SetorId = dto.SetorId,
                // Para manter legado de senha/token vazio (ajustar depois conforme autenticação):
                Senha = string.Empty,
                TokenEmpresa = string.Empty
            };
        }
    }
}