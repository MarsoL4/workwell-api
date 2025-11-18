using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.ApoioPsicologico;

namespace WorkWell.API.SwaggerExamples
{
    public class PsicologoDtoExample : IExamplesProvider<PsicologoDto>
    {
        public PsicologoDto GetExamples()
        {
            return new PsicologoDto
            {
                Nome = "Dra. Helena Alves",
                Email = "helena.alves@workwell.com",
                Crp = "06/123456",
                Ativo = true,
                SetorId = 2
            };
        }
    }
}