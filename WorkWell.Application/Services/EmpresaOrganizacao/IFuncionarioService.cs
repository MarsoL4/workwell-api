using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.EmpresaOrganizacao;

namespace WorkWell.Application.Services.EmpresaOrganizacao
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<FuncionarioDto>> GetAllAsync();
        Task<FuncionarioDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(FuncionarioDto funcionarioDto);
        Task UpdateAsync(FuncionarioDto funcionarioDto);
        Task DeleteAsync(long id);
    }
}