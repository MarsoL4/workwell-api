using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;
using WorkWell.Domain.Entities.ApoioPsicologico;

namespace WorkWell.Application.Services.ApoioPsicologico
{
    public class PsicologoService : IPsicologoService
    {
        private readonly IPsicologoRepository _repo;

        public PsicologoService(IPsicologoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PsicologoDto>> GetAllAsync()
        {
            var models = await _repo.GetAllAsync();
            var dtos = new List<PsicologoDto>();
            foreach (var m in models) dtos.Add(ToDto(m));
            return dtos;
        }

        public async Task<PsicologoDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<long> CreateAsync(PsicologoDto dto)
        {
            var entity = FromDto(dto);
            await _repo.AddAsync(entity);
            return entity.Id;
        }

        public async Task UpdateAsync(PsicologoDto dto)
        {
            var entity = FromDto(dto);
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
        }

        private static PsicologoDto ToDto(Psicologo e) => new()
        {
            Id = e.Id,
            Nome = e.Nome,
            Email = e.Email,
            Crp = e.Crp,
            Ativo = e.Ativo,
            SetorId = e.SetorId
        };

        private static Psicologo FromDto(PsicologoDto d) => new()
        {
            Id = d.Id,
            Nome = d.Nome,
            Email = d.Email,
            Crp = d.Crp,
            Ativo = d.Ativo,
            SetorId = d.SetorId
        };
    }
}