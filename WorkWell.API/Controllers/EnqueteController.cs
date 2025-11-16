using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.Enquetes;
using WorkWell.Application.Services.Enquetes;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnqueteController : ControllerBase
    {
        private readonly IEnqueteService _enqueteService;

        public EnqueteController(IEnqueteService enqueteService)
        {
            _enqueteService = enqueteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnqueteDto>>> GetAll()
        {
            var enquetes = await _enqueteService.GetAllAsync();
            return Ok(enquetes);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<EnqueteDto>> GetById(long id)
        {
            var enquete = await _enqueteService.GetByIdAsync(id);
            if (enquete == null)
                return NotFound();
            return Ok(enquete);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(EnqueteDto dto)
        {
            var id = await _enqueteService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, EnqueteDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");
            await _enqueteService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _enqueteService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{enqueteId:long}/respostas")]
        public async Task<ActionResult<IEnumerable<RespostaEnqueteDto>>> GetRespostas(long enqueteId)
        {
            var respostas = await _enqueteService.GetRespostasAsync(enqueteId);
            return Ok(respostas);
        }

        [HttpPost("{enqueteId:long}/respostas")]
        public async Task<ActionResult<long>> AdicionarResposta(long enqueteId, RespostaEnqueteDto dto)
        {
            var id = await _enqueteService.AdicionarRespostaAsync(enqueteId, dto);
            return Ok(id);
        }
    }
}