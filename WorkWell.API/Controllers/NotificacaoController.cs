using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.Notificacoes;
using WorkWell.Application.Services.Notificacoes;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoService _notificacaoService;

        public NotificacaoController(INotificacaoService notificacaoService)
        {
            _notificacaoService = notificacaoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificacaoDto>>> GetAll()
        {
            var notificacoes = await _notificacaoService.GetAllAsync();
            return Ok(notificacoes);
        }

        [HttpGet("funcionario/{funcionarioId:long}")]
        public async Task<ActionResult<IEnumerable<NotificacaoDto>>> GetByFuncionario(long funcionarioId)
        {
            var notificacoes = await _notificacaoService.GetAllByFuncionarioIdAsync(funcionarioId);
            return Ok(notificacoes);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<NotificacaoDto>> GetById(long id)
        {
            var notificacao = await _notificacaoService.GetByIdAsync(id);
            if (notificacao == null)
                return NotFound();
            return Ok(notificacao);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(NotificacaoDto dto)
        {
            var id = await _notificacaoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, NotificacaoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _notificacaoService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _notificacaoService.DeleteAsync(id);
            return NoContent();
        }
    }
}