using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AtividadesBemEstar;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class ParticipacaoAtividadeDtoExample : IExamplesProvider<ParticipacaoAtividadeDto>
    {
        public ParticipacaoAtividadeDto GetExamples()
        {
            return new ParticipacaoAtividadeDto
            {
                Id = 1,
                FuncionarioId = 100,
                AtividadeId = 5,
                Participou = true,
                DataParticipacao = DateTime.UtcNow
            };
        }
    }
}