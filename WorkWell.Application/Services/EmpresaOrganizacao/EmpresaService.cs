using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;
using System.Linq;

namespace WorkWell.Application.Services.EmpresaOrganizacao
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<IEnumerable<EmpresaDto>> GetAllAsync()
        {
            var empresas = await _empresaRepository.GetAllAsync();
            var dtos = new List<EmpresaDto>();

            foreach (var empresa in empresas)
            {
                dtos.Add(ToDto(empresa));
            }
            return dtos;
        }

        public async Task<PagedResultDto<EmpresaDto>> GetAllPagedAsync(int page, int pageSize)
        {
            var (items, total) = await _empresaRepository.GetAllPagedAsync(page, pageSize);
            return new PagedResultDto<EmpresaDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = items.Select(ToDto).ToList()
            };
        }

        public async Task<EmpresaDto?> GetByIdAsync(long id)
        {
            var empresa = await _empresaRepository.GetByIdAsync(id);
            return empresa == null ? null : ToDto(empresa);
        }

        public async Task<long> CreateAsync(EmpresaDto empresaDto)
        {
            var empresa = new Empresa
            {
                Nome = empresaDto.Nome,
                EmailAdmin = empresaDto.EmailAdmin,
                SenhaAdmin = empresaDto.SenhaAdmin,
                TokenAcesso = empresaDto.TokenAcesso,
                LogoUrl = empresaDto.LogoUrl,
                CorPrimaria = empresaDto.CorPrimaria,
                CorSecundaria = empresaDto.CorSecundaria,
                Missao = empresaDto.Missao,
                PoliticaBemEstar = empresaDto.PoliticaBemEstar
            };
            await _empresaRepository.AddAsync(empresa);
            return empresa.Id;
        }

        public async Task UpdateAsync(EmpresaDto empresaDto)
        {
            var empresa = new Empresa
            {
                Id = empresaDto.Id,
                Nome = empresaDto.Nome,
                EmailAdmin = empresaDto.EmailAdmin,
                SenhaAdmin = empresaDto.SenhaAdmin,
                TokenAcesso = empresaDto.TokenAcesso,
                LogoUrl = empresaDto.LogoUrl,
                CorPrimaria = empresaDto.CorPrimaria,
                CorSecundaria = empresaDto.CorSecundaria,
                Missao = empresaDto.Missao,
                PoliticaBemEstar = empresaDto.PoliticaBemEstar
            };
            await _empresaRepository.UpdateAsync(empresa);
        }

        public async Task DeleteAsync(long id)
        {
            await _empresaRepository.DeleteAsync(id);
        }

        private static EmpresaDto ToDto(Empresa empresa)
        {
            return new EmpresaDto
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                EmailAdmin = empresa.EmailAdmin,
                SenhaAdmin = empresa.SenhaAdmin,
                TokenAcesso = empresa.TokenAcesso,
                LogoUrl = empresa.LogoUrl,
                CorPrimaria = empresa.CorPrimaria,
                CorSecundaria = empresa.CorSecundaria,
                Missao = empresa.Missao,
                PoliticaBemEstar = empresa.PoliticaBemEstar
            };
        }
    }
}