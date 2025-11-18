using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class PerfilEmocionalDtoExample : IExamplesProvider<PerfilEmocionalDto>
    {
        public PerfilEmocionalDto GetExamples()
        {
            return new PerfilEmocionalDto
            {
                FuncionarioId = 101,
                HumorInicial = "Bem",
                Rotina = "Acorda às 7h, trabalha até 18h, faz caminhada no final do dia.",
                PrincipaisEstressores = "Demandas em excesso e prazos curtos.",
                DataCriacao = DateTime.UtcNow
            };
        }
    }
}