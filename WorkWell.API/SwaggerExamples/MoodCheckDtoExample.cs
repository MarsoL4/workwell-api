using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class MoodCheckDtoExample : IExamplesProvider<MoodCheckDto>
    {
        public MoodCheckDto GetExamples()
        {
            return new MoodCheckDto
            {
                Id = 1,
                FuncionarioId = 101,
                Humor = 4,
                Produtivo = true,
                Estressado = false,
                DormiuBem = true,
                DataRegistro = DateTime.UtcNow
            };
        }
    }
}