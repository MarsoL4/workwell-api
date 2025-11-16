using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.Notificacoes;

namespace WorkWell.Application.Services.Notificacoes
{
    public interface INotificacaoService
    {
        Task<IEnumerable<NotificacaoDto>> GetAllAsync();
        Task<NotificacaoDto?> GetByIdAsync(long id);
        Task<IEnumerable<NotificacaoDto>> GetAllByFuncionarioIdAsync(long funcionarioId);
        Task<long> CreateAsync(NotificacaoDto dto);
        Task UpdateAsync(NotificacaoDto dto);
        Task DeleteAsync(long id);
    }
}