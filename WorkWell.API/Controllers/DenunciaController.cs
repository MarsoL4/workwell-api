using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.OmbudMind;
using WorkWell.Application.Services.OmbudMind;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DenunciaController : ControllerBase
    {
        private readonly IDenunciaService _denunciaService;

        public DenunciaController(IDenunciaService denunciaService)
        {
            _denunciaService = denunciaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DenunciaDto>>> GetAll()
        {
            var list = await _denunciaService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<DenunciaDto>> GetById(long id)
        {
            var entity = await _denunciaService.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<DenunciaDto>> GetByCodigoRastreamento(string codigo)
        {
            var entity = await _denunciaService.GetByCodigoRastreamentoAsync(codigo);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(DenunciaDto dto)
        {
            var id = await _denunciaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, DenunciaDto dto)
        {
            if (id != dto.Id) return BadRequest();
            await _denunciaService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _denunciaService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{denunciaId:long}/investigacoes")]
        public async Task<ActionResult<IEnumerable<InvestigacaoDenunciaDto>>> GetInvestigacoes(long denunciaId)
        {
            var list = await _denunciaService.GetInvestigacoesAsync(denunciaId);
            return Ok(list);
        }

        [HttpPost("{denunciaId:long}/investigacoes")]
        public async Task<ActionResult<long>> AdicionarInvestigacao(long denunciaId, InvestigacaoDenunciaDto dto)
        {
            var id = await _denunciaService.AdicionarInvestigacaoAsync(denunciaId, dto);
            return Ok(id);
        }
    }
}