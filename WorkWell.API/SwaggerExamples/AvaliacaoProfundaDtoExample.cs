using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class AvaliacaoProfundaDtoExample : IExamplesProvider<AvaliacaoProfundaDto>
    {
        public AvaliacaoProfundaDto GetExamples()
        {
            return new AvaliacaoProfundaDto
            {
                FuncionarioId = 101,
                Gad7Score = 4,
                Phq9Score = 6,
                Interpretacao = "Ansiedade leve, depressão leve.",
                DataRegistro = DateTime.UtcNow
            };
        }
    }
}