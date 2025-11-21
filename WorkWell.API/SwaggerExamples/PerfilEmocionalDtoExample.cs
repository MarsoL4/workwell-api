using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;

namespace WorkWell.API.SwaggerExamples
{
    public class PerfilEmocionalDtoExample : IExamplesProvider<PerfilEmocionalDto>
    {
        public PerfilEmocionalDto GetExamples()
        {
            return new PerfilEmocionalDto
            {
                FuncionarioId = 4, // Carlos Silva
                HumorInicial = "Bem",
                Rotina = "Home office, faz exercícios matinais.",
                PrincipaisEstressores = "Excesso de reuniões."
            };
        }
    }
}