using Microsoft.AspNetCore.Mvc;
using WorkWell.Application.DTOs.Indicadores;
using WorkWell.Domain.Entities.Indicadores;
using WorkWell.Domain.Interfaces.Indicadores;

namespace WorkWell.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndicadoresEmpresaController : ControllerBase
    {
        private readonly IIndicadoresEmpresaRepository _repository;

        public IndicadoresEmpresaController(IIndicadoresEmpresaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IndicadoresEmpresaDto>>> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return Ok(list.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<IndicadoresEmpresaDto>> GetById(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(ToDto(entity));
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(IndicadoresEmpresaDto dto)
        {
            var entity = FromDto(dto);
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.Id);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, IndicadoresEmpresaDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var entity = FromDto(dto);
            await _repository.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private static IndicadoresEmpresaDto ToDto(IndicadoresEmpresa e) => new()
        {
            Id = e.Id,
            EmpresaId = e.EmpresaId,
            HumorMedio = e.HumorMedio,
            AdesaoAtividadesGeral = e.AdesaoAtividadesGeral,
            AdesaoPorSetor = e.AdesaoPorSetor.Select(a => new AdesaoSetorDto
            {
                Id = a.Id,
                SetorId = a.SetorId,
                Adesao = a.Adesao
            }).ToList(),
            FrequenciaConsultas = e.FrequenciaConsultas
        };

        private static IndicadoresEmpresa FromDto(IndicadoresEmpresaDto dto) => new()
        {
            Id = dto.Id,
            EmpresaId = dto.EmpresaId,
            HumorMedio = dto.HumorMedio,
            AdesaoAtividadesGeral = dto.AdesaoAtividadesGeral,
            AdesaoPorSetor = dto.AdesaoPorSetor.ConvertAll(a => new Domain.Entities.Indicadores.AdesaoSetor
            {
                Id = a.Id,
                SetorId = a.SetorId,
                Adesao = a.Adesao
            }),
            FrequenciaConsultas = dto.FrequenciaConsultas
        };
    }
}