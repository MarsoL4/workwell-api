using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.EmpresaOrganizacao;

namespace WorkWell.Application.Services.EmpresaOrganizacao
{
    public interface IEmpresaService
    {
        Task<IEnumerable<EmpresaDto>> GetAllAsync();
        Task<EmpresaDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(EmpresaDto empresaDto);
        Task UpdateAsync(EmpresaDto empresaDto);
        Task DeleteAsync(long id);
    }
}