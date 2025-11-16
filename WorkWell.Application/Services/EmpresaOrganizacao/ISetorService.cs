using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.EmpresaOrganizacao;

namespace WorkWell.Application.Services.EmpresaOrganizacao
{
    public interface ISetorService
    {
        Task<IEnumerable<SetorDto>> GetAllAsync();
        Task<SetorDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(SetorDto setorDto);
        Task UpdateAsync(SetorDto setorDto);
        Task DeleteAsync(long id);
    }
}