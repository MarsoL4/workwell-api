using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.EmpresaOrganizacao;

namespace WorkWell.API.SwaggerExamples
{
    public class EmpresaDtoExample : IExamplesProvider<EmpresaDto>
    {
        public EmpresaDto GetExamples()
        {
            return new EmpresaDto
            {
                Nome = "Exemplo Ltda",
                EmailAdmin = "admin@exemplo.com",
                TokenAcesso = "token-exemplo-12345",
                LogoUrl = "https://exemplo.com/logo.png",
                CorPrimaria = "#0055CC",
                CorSecundaria = "#FFB800",
                Missao = "Nossa missão é promover saúde em todos os níveis.",
                PoliticaBemEstar = "Política clara de respeito e inclusão em toda a empresa."
            };
        }
    }
}