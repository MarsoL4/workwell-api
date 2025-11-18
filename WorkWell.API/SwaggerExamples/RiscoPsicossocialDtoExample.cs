using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class RiscoPsicossocialDtoExample : IExamplesProvider<RiscoPsicossocialDto>
    {
        public RiscoPsicossocialDto GetExamples()
        {
            return new RiscoPsicossocialDto
            {
                FuncionarioId = 101,
                Categoria = "Sobrecarga de trabalho",
                NivelRisco = 3,
                DataRegistro = DateTime.UtcNow
            };
        }
    }
}