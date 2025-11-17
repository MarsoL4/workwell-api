using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Application.Services.ApoioPsicologico;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ChatAnonimoController : ControllerBase
    {
        private readonly IChatAnonimoService _chatService;

        public ChatAnonimoController(IChatAnonimoService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatAnonimoDto>>> GetAll()
        {
            var chats = await _chatService.GetAllAsync();
            return Ok(chats);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ChatAnonimoDto>> GetById(long id)
        {
            var chat = await _chatService.GetByIdAsync(id);
            if (chat == null)
                return NotFound();
            return Ok(chat);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(ChatAnonimoDto dto)
        {
            var id = await _chatService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, ChatAnonimoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            await _chatService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _chatService.DeleteAsync(id);
            return NoContent();
        }
    }
}