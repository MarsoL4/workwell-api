using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.AtividadesBemEstar;
using WorkWell.Application.Services.AtividadesBemEstar;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AtividadeBemEstarController : ControllerBase
    {
        private readonly IAtividadeBemEstarService _atividadeService;

        public AtividadeBemEstarController(IAtividadeBemEstarService atividadeService)
        {
            _atividadeService = atividadeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AtividadeBemEstarDto>>> GetAll()
        {
            var atividades = await _atividadeService.GetAllAsync();
            return Ok(atividades);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<AtividadeBemEstarDto>> GetById(long id)
        {
            var atividade = await _atividadeService.GetByIdAsync(id);
            if (atividade == null)
                return NotFound();
            return Ok(atividade);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(AtividadeBemEstarDto dto)
        {
            var id = await _atividadeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, AtividadeBemEstarDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _atividadeService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _atividadeService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{atividadeId:long}/participacoes")]
        public async Task<ActionResult<IEnumerable<ParticipacaoAtividadeDto>>> GetParticipacoes(long atividadeId)
        {
            var participacoes = await _atividadeService.GetParticipacoesAsync(atividadeId);
            return Ok(participacoes);
        }

        [HttpPost("{atividadeId:long}/participacoes")]
        public async Task<ActionResult<long>> Participar(long atividadeId, ParticipacaoAtividadeDto dto)
        {
            var id = await _atividadeService.AdicionarParticipacaoAsync(atividadeId, dto);
            return Ok(id);
        }
    }
}