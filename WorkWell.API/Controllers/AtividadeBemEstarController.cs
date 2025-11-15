using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.AtividadesBemEstar;
using WorkWell.Domain.Entities.AtividadesBemEstar;
using WorkWell.Domain.Interfaces.AtividadesBemEstar;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtividadeBemEstarController : ControllerBase
    {
        private readonly IAtividadeBemEstarRepository _atividadeRepository;
        private readonly IParticipacaoAtividadeRepository _participacaoRepository;

        public AtividadeBemEstarController(IAtividadeBemEstarRepository atividadeRepository, IParticipacaoAtividadeRepository participacaoRepository)
        {
            _atividadeRepository = atividadeRepository;
            _participacaoRepository = participacaoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AtividadeBemEstarDto>>> GetAll()
        {
            var atividades = await _atividadeRepository.GetAllAsync();
            return Ok(atividades.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<AtividadeBemEstarDto>> GetById(long id)
        {
            var atividade = await _atividadeRepository.GetByIdAsync(id);
            if (atividade == null)
                return NotFound();
            return Ok(ToDto(atividade));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(AtividadeBemEstarDto dto)
        {
            var atividade = FromDto(dto);
            await _atividadeRepository.AddAsync(atividade);
            return CreatedAtAction(nameof(GetById), new { id = atividade.Id }, atividade.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, AtividadeBemEstarDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da URL e do objeto devem coincidir.");

            var atividade = FromDto(dto);
            await _atividadeRepository.UpdateAsync(atividade);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _atividadeRepository.DeleteAsync(id);
            return NoContent();
        }

        // Participação rotas

        [HttpGet("{atividadeId:long}/participacoes")]
        public async Task<ActionResult<IEnumerable<ParticipacaoAtividadeDto>>> GetParticipacoes(long atividadeId)
        {
            var participacoes = await _participacaoRepository.GetAllByAtividadeIdAsync(atividadeId);
            return Ok(participacoes.Select(ToDto));
        }

        [HttpPost("{atividadeId:long}/participacoes")]
        public async Task<ActionResult<long>> Participar(long atividadeId, ParticipacaoAtividadeDto dto)
        {
            var part = FromDto(dto);
            part.AtividadeId = atividadeId;
            await _participacaoRepository.AddAsync(part);
            return Ok(part.Id);
        }

        // Conversion helpers

        private static AtividadeBemEstarDto ToDto(AtividadeBemEstar entidade) =>
            new()
            {
                Id = entidade.Id,
                EmpresaId = entidade.EmpresaId,
                Tipo = entidade.Tipo,
                Titulo = entidade.Titulo,
                Descricao = entidade.Descricao,
                DataInicio = entidade.DataInicio,
                DataFim = entidade.DataFim,
                SetorAlvoId = entidade.SetorAlvoId
            };

        private static AtividadeBemEstar FromDto(AtividadeBemEstarDto dto) =>
            new()
            {
                Id = dto.Id,
                EmpresaId = dto.EmpresaId,
                Tipo = dto.Tipo,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                SetorAlvoId = dto.SetorAlvoId
            };

        private static ParticipacaoAtividadeDto ToDto(ParticipacaoAtividade entidade) =>
            new()
            {
                Id = entidade.Id,
                FuncionarioId = entidade.FuncionarioId,
                AtividadeId = entidade.AtividadeId,
                Participou = entidade.Participou,
                DataParticipacao = entidade.DataParticipacao
            };

        private static ParticipacaoAtividade FromDto(ParticipacaoAtividadeDto dto) =>
            new()
            {
                Id = dto.Id,
                FuncionarioId = dto.FuncionarioId,
                AtividadeId = dto.AtividadeId,
                Participou = dto.Participou,
                DataParticipacao = dto.DataParticipacao
            };
    }
}