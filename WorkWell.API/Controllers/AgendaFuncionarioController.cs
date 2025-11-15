using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.Agenda;
using WorkWell.Domain.Entities.Agenda;
using WorkWell.Domain.Interfaces.Agenda;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaFuncionarioController : ControllerBase
    {
        private readonly IAgendaFuncionarioRepository _agendaRepository;
        private readonly IItemAgendaRepository _itemRepository;

        public AgendaFuncionarioController(IAgendaFuncionarioRepository agendaRepository, IItemAgendaRepository itemRepository)
        {
            _agendaRepository = agendaRepository;
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendaFuncionarioDto>>> GetAll()
        {
            var agendas = await _agendaRepository.GetAllAsync();
            return Ok(agendas.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<AgendaFuncionarioDto>> GetById(long id)
        {
            var agenda = await _agendaRepository.GetByIdAsync(id);
            if (agenda == null)
                return NotFound();
            return Ok(ToDto(agenda));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(AgendaFuncionarioDto dto)
        {
            var agenda = FromDto(dto);
            await _agendaRepository.AddAsync(agenda);
            return CreatedAtAction(nameof(GetById), new { id = agenda.Id }, agenda.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, AgendaFuncionarioDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            var agenda = FromDto(dto);
            await _agendaRepository.UpdateAsync(agenda);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _agendaRepository.DeleteAsync(id);
            return NoContent();
        }

        // Itens da agenda - rotas filhas

        [HttpGet("{agendaId:long}/itens")]
        public async Task<ActionResult<IEnumerable<ItemAgendaDto>>> GetItens(long agendaId)
        {
            var itens = await _itemRepository.GetAllByAgendaIdAsync(agendaId);
            return Ok(itens.Select(ToDto));
        }

        [HttpPost("{agendaId:long}/itens")]
        public async Task<ActionResult<long>> AdicionarItem(long agendaId, ItemAgendaDto dto)
        {
            var item = FromDto(dto);
            item.AgendaFuncionarioId = agendaId;
            await _itemRepository.AddAsync(item);
            return Ok(item.Id);
        }

        // Converters

        private static AgendaFuncionarioDto ToDto(AgendaFuncionario entidade) =>
            new()
            {
                Id = entidade.Id,
                FuncionarioId = entidade.FuncionarioId,
                Data = entidade.Data,
                Itens = entidade.Itens.Select(ToDto).ToList()
            };

        private static AgendaFuncionario FromDto(AgendaFuncionarioDto dto) =>
            new()
            {
                Id = dto.Id,
                FuncionarioId = dto.FuncionarioId,
                Data = dto.Data,
                Itens = dto.Itens.Select(FromDto).ToList()
            };

        private static ItemAgendaDto ToDto(ItemAgenda entidade) =>
            new()
            {
                Id = entidade.Id,
                AgendaFuncionarioId = entidade.AgendaFuncionarioId,
                Tipo = entidade.Tipo,
                Titulo = entidade.Titulo,
                Horario = entidade.Horario
            };

        private static ItemAgenda FromDto(ItemAgendaDto dto) =>
            new()
            {
                Id = dto.Id,
                AgendaFuncionarioId = dto.AgendaFuncionarioId,
                Tipo = dto.Tipo,
                Titulo = dto.Titulo,
                Horario = dto.Horario
            };
    }
}