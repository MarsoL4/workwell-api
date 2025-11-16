using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.OmbudMind;
using WorkWell.Domain.Entities.OmbudMind;
using WorkWell.Domain.Interfaces.OmbudMind;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DenunciaController : ControllerBase
    {
        private readonly IDenunciaRepository _denunciaRepo;
        private readonly IInvestigacaoDenunciaRepository _investigacaoRepo;

        public DenunciaController(
            IDenunciaRepository denunciaRepo,
            IInvestigacaoDenunciaRepository investigacaoRepo)
        {
            _denunciaRepo = denunciaRepo;
            _investigacaoRepo = investigacaoRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DenunciaDto>>> GetAll()
        {
            var list = await _denunciaRepo.GetAllAsync();
            return Ok(list.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<DenunciaDto>> GetById(long id)
        {
            var entity = await _denunciaRepo.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(ToDto(entity));
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<DenunciaDto>> GetByCodigoRastreamento(string codigo)
        {
            var entity = await _denunciaRepo.GetByCodigoRastreamentoAsync(codigo);
            if (entity == null) return NotFound();
            return Ok(ToDto(entity));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(DenunciaDto dto)
        {
            var entity = FromDto(dto);
            await _denunciaRepo.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, DenunciaDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var entity = FromDto(dto);
            await _denunciaRepo.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _denunciaRepo.DeleteAsync(id);
            return NoContent();
        }

        // INVESTIGAÇÃO

        [HttpGet("{denunciaId:long}/investigacoes")]
        public async Task<ActionResult<IEnumerable<InvestigacaoDenunciaDto>>> GetInvestigacoes(long denunciaId)
        {
            var list = await _investigacaoRepo.GetAllByDenunciaIdAsync(denunciaId);
            return Ok(list.Select(ToDto));
        }

        [HttpPost("{denunciaId:long}/investigacoes")]
        public async Task<ActionResult<long>> AdicionarInvestigacao(long denunciaId, InvestigacaoDenunciaDto dto)
        {
            var entity = FromDto(dto);
            entity.DenunciaId = denunciaId;
            await _investigacaoRepo.AddAsync(entity);
            return Ok(entity.Id);
        }

        private static DenunciaDto ToDto(Denuncia d) => new()
        {
            Id = d.Id,
            FuncionarioDenuncianteId = d.FuncionarioDenuncianteId,
            EmpresaId = d.EmpresaId,
            Tipo = d.Tipo,
            Descricao = d.Descricao,
            DataCriacao = d.DataCriacao,
            Status = d.Status,
            CodigoRastreamento = d.CodigoRastreamento
        };

        private static Denuncia FromDto(DenunciaDto dto) => new()
        {
            Id = dto.Id,
            FuncionarioDenuncianteId = dto.FuncionarioDenuncianteId,
            EmpresaId = dto.EmpresaId,
            Tipo = dto.Tipo,
            Descricao = dto.Descricao,
            DataCriacao = dto.DataCriacao,
            Status = dto.Status,
            CodigoRastreamento = dto.CodigoRastreamento
        };

        private static InvestigacaoDenunciaDto ToDto(InvestigacaoDenuncia i) => new()
        {
            Id = i.Id,
            DenunciaId = i.DenunciaId,
            EquipeResponsavel = i.EquipeResponsavel,
            DataInicio = i.DataInicio,
            DataFim = i.DataFim,
            MedidasAdotadas = i.MedidasAdotadas,
            Concluida = i.Concluida
        };

        private static InvestigacaoDenuncia FromDto(InvestigacaoDenunciaDto dto) => new()
        {
            Id = dto.Id,
            DenunciaId = dto.DenunciaId,
            EquipeResponsavel = dto.EquipeResponsavel,
            DataInicio = dto.DataInicio,
            DataFim = dto.DataFim,
            MedidasAdotadas = dto.MedidasAdotadas,
            Concluida = dto.Concluida
        };
    }
}