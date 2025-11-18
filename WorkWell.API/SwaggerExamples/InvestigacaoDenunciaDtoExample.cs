using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.OmbudMind;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class InvestigacaoDenunciaDtoExample : IExamplesProvider<InvestigacaoDenunciaDto>
    {
        public InvestigacaoDenunciaDto GetExamples()
        {
            return new InvestigacaoDenunciaDto
            {
                DenunciaId = 1,
                EquipeResponsavel = "RH",
                DataInicio = DateTime.UtcNow,
                DataFim = null,
                MedidasAdotadas = "Afastamento provisório do acusado.",
                Concluida = false
            };
        }
    }
}