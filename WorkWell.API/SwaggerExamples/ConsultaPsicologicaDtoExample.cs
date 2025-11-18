using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Enums.ApoioPsicologico;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class ConsultaPsicologicaDtoExample : IExamplesProvider<ConsultaPsicologicaDto>
    {
        public ConsultaPsicologicaDto GetExamples()
        {
            return new ConsultaPsicologicaDto
            {
                FuncionarioId = 100,
                PsicologoId = 200,
                DataConsulta = DateTime.UtcNow,
                Tipo = TipoConsulta.Online,
                Status = StatusConsulta.Agendada,
                AnotacoesSigilosas = "Primeiro atendimento, traz sintomas de ansiedade."
            };
        }
    }
}