using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.Enquetes;

namespace WorkWell.Application.Services.Enquetes
{
    public interface IEnqueteService
    {
        Task<IEnumerable<EnqueteDto>> GetAllAsync();
        Task<EnqueteDto?> GetByIdAsync(long id);
        Task<long> CreateAsync(EnqueteDto dto);
        Task UpdateAsync(EnqueteDto dto);
        Task DeleteAsync(long id);

        Task<IEnumerable<RespostaEnqueteDto>> GetRespostasAsync(long enqueteId);
        Task<long> AdicionarRespostaAsync(long enqueteId, RespostaEnqueteDto dto);
    }
}