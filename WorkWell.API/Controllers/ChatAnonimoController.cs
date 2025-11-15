using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatAnonimoController : ControllerBase
    {
        private readonly IChatAnonimoRepository _repository;

        public ChatAnonimoController(IChatAnonimoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatAnonimoDto>>> GetAll()
        {
            var chats = await _repository.GetAllAsync();
            return Ok(chats.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ChatAnonimoDto>> GetById(long id)
        {
            var chat = await _repository.GetByIdAsync(id);
            if (chat == null)
                return NotFound();
            return Ok(ToDto(chat));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(ChatAnonimoDto dto)
        {
            var chat = FromDto(dto);
            await _repository.AddAsync(chat);
            return CreatedAtAction(nameof(GetById), new { id = chat.Id }, chat.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, ChatAnonimoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            var chat = FromDto(dto);
            await _repository.UpdateAsync(chat);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private static ChatAnonimoDto ToDto(ChatAnonimo entidade) =>
            new()
            {
                Id = entidade.Id,
                RemetenteId = entidade.RemetenteId,
                PsicologoId = entidade.PsicologoId,
                Mensagem = entidade.Mensagem,
                DataEnvio = entidade.DataEnvio,
                Anonimo = entidade.Anonimo
            };

        private static ChatAnonimo FromDto(ChatAnonimoDto dto) =>
            new()
            {
                Id = dto.Id,
                RemetenteId = dto.RemetenteId,
                PsicologoId = dto.PsicologoId,
                Mensagem = dto.Mensagem,
                DataEnvio = dto.DataEnvio,
                Anonimo = dto.Anonimo
            };
    }
}