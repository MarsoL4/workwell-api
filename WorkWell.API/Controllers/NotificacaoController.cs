using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.Notificacoes;
using WorkWell.Domain.Entities.Notificacoes;
using WorkWell.Domain.Interfaces.Notificacoes;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoRepository _repository;

        public NotificacaoController(INotificacaoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificacaoDto>>> GetAll()
        {
            var notificacoes = await _repository.GetAllAsync();
            return Ok(notificacoes.Select(ToDto));
        }

        [HttpGet("funcionario/{funcionarioId:long}")]
        public async Task<ActionResult<IEnumerable<NotificacaoDto>>> GetByFuncionario(long funcionarioId)
        {
            var notificacoes = await _repository.GetAllByFuncionarioIdAsync(funcionarioId);
            return Ok(notificacoes.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<NotificacaoDto>> GetById(long id)
        {
            var notificacao = await _repository.GetByIdAsync(id);
            if (notificacao == null)
                return NotFound();
            return Ok(ToDto(notificacao));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(NotificacaoDto dto)
        {
            var notificacao = FromDto(dto);
            await _repository.AddAsync(notificacao);
            return CreatedAtAction(nameof(GetById), new { id = notificacao.Id }, notificacao.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, NotificacaoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            var notificacao = FromDto(dto);
            await _repository.UpdateAsync(notificacao);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private static NotificacaoDto ToDto(Notificacao entidade) =>
            new()
            {
                Id = entidade.Id,
                FuncionarioId = entidade.FuncionarioId,
                Mensagem = entidade.Mensagem,
                Tipo = entidade.Tipo,
                Lida = entidade.Lida,
                DataEnvio = entidade.DataEnvio
            };

        private static Notificacao FromDto(NotificacaoDto dto) =>
            new()
            {
                Id = dto.Id,
                FuncionarioId = dto.FuncionarioId,
                Mensagem = dto.Mensagem,
                Tipo = dto.Tipo,
                Lida = dto.Lida,
                DataEnvio = dto.DataEnvio
            };
    }
}