using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Domain.Enums.EmpresaOrganizacao;

namespace WorkWell.API.SwaggerExamples
{
    public class FuncionarioDtoExample : IExamplesProvider<FuncionarioDto>
    {
        public FuncionarioDto GetExamples()
        {
            return new FuncionarioDto
            {
                Id = 100,
                Nome = "Maria Oliveira",
                Email = "maria.oliveira@exemplo.com",
                Cargo = Cargo.Funcionario,
                Ativo = true,
                SetorId = 2
            };
        }
    }
}