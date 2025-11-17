using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.Agenda;
using WorkWell.Application.Services.Agenda;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AgendaFuncionarioController : ControllerBase
    {
        private readonly IAgendaFuncionarioService _agendaService;

        public AgendaFuncionarioController(IAgendaFuncionarioService agendaService)
        {
            _agendaService = agendaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendaFuncionarioDto>>> GetAll()
        {
            var agendas = await _agendaService.GetAllAsync();
            return Ok(agendas);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<AgendaFuncionarioDto>> GetById(long id)
        {
            var agenda = await _agendaService.GetByIdAsync(id);
            if (agenda == null)
                return NotFound();
            return Ok(agenda);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(AgendaFuncionarioDto dto)
        {
            var id = await _agendaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, AgendaFuncionarioDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _agendaService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _agendaService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{agendaId:long}/itens")]
        public async Task<ActionResult<IEnumerable<ItemAgendaDto>>> GetItens(long agendaId)
        {
            var itens = await _agendaService.GetItensAsync(agendaId);
            return Ok(itens);
        }

        [HttpPost("{agendaId:long}/itens")]
        public async Task<ActionResult<long>> AdicionarItem(long agendaId, ItemAgendaDto dto)
        {
            var id = await _agendaService.AdicionarItemAsync(agendaId, dto);
            return Ok(id);
        }
    }
}