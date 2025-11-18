using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.ApoioPsicologico;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class SOSemergenciaDtoExample : IExamplesProvider<SOSemergenciaDto>
    {
        public SOSemergenciaDto GetExamples()
        {
            return new SOSemergenciaDto
            {
                FuncionarioId = 101,
                DataAcionamento = DateTime.UtcNow,
                Tipo = "Crise de ansiedade",
                PsicologoNotificado = true
            };
        }
    }
}