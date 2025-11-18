using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.EmpresaOrganizacao;

namespace WorkWell.API.SwaggerExamples
{
    public class SetorDtoExample : IExamplesProvider<SetorDto>
    {
        public SetorDto GetExamples()
        {
            return new SetorDto
            {
                Nome = "Recursos Humanos",
                EmpresaId = 1
            };
        }
    }
}