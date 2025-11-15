using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.Enquetes;
using WorkWell.Domain.Entities.Enquetes;
using WorkWell.Domain.Interfaces.Enquetes;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnqueteController : ControllerBase
    {
        private readonly IEnqueteRepository _enqueteRepository;
        private readonly IRespostaEnqueteRepository _respostaRepository;

        public EnqueteController(IEnqueteRepository enqueteRepository, IRespostaEnqueteRepository respostaRepository)
        {
            _enqueteRepository = enqueteRepository;
            _respostaRepository = respostaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnqueteDto>>> GetAll()
        {
            var enquetes = await _enqueteRepository.GetAllAsync();
            return Ok(enquetes.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<EnqueteDto>> GetById(long id)
        {
            var enquete = await _enqueteRepository.GetByIdAsync(id);
            if (enquete == null)
                return NotFound();
            return Ok(ToDto(enquete));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(EnqueteDto dto)
        {
            var enquete = FromDto(dto);
            await _enqueteRepository.AddAsync(enquete);
            return CreatedAtAction(nameof(GetById), new { id = enquete.Id }, enquete.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, EnqueteDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");
            var enquete = FromDto(dto);
            await _enqueteRepository.UpdateAsync(enquete);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _enqueteRepository.DeleteAsync(id);
            return NoContent();
        }

        // Rotas de resposta de enquete

        [HttpGet("{enqueteId:long}/respostas")]
        public async Task<ActionResult<IEnumerable<RespostaEnqueteDto>>> GetRespostas(long enqueteId)
        {
            var respostas = await _respostaRepository.GetAllByEnqueteIdAsync(enqueteId);
            return Ok(respostas.Select(ToDto));
        }

        [HttpPost("{enqueteId:long}/respostas")]
        public async Task<ActionResult<long>> AdicionarResposta(long enqueteId, RespostaEnqueteDto dto)
        {
            var resposta = FromDto(dto);
            resposta.EnqueteId = enqueteId;
            await _respostaRepository.AddAsync(resposta);
            return Ok(resposta.Id);
        }

        private static EnqueteDto ToDto(Enquete entidade) =>
            new()
            {
                Id = entidade.Id,
                EmpresaId = entidade.EmpresaId,
                Pergunta = entidade.Pergunta,
                DataCriacao = entidade.DataCriacao,
                Ativa = entidade.Ativa
            };

        private static Enquete FromDto(EnqueteDto dto) =>
            new()
            {
                Id = dto.Id,
                EmpresaId = dto.EmpresaId,
                Pergunta = dto.Pergunta,
                DataCriacao = dto.DataCriacao,
                Ativa = dto.Ativa
            };

        private static RespostaEnqueteDto ToDto(RespostaEnquete entidade) =>
            new()
            {
                Id = entidade.Id,
                EnqueteId = entidade.EnqueteId,
                FuncionarioId = entidade.FuncionarioId,
                Resposta = entidade.Resposta
            };

        private static RespostaEnquete FromDto(RespostaEnqueteDto dto) =>
            new()
            {
                Id = dto.Id,
                EnqueteId = dto.EnqueteId,
                FuncionarioId = dto.FuncionarioId,
                Resposta = dto.Resposta
            };
    }
}