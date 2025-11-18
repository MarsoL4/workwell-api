using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.Enquetes;

namespace WorkWell.API.SwaggerExamples
{
    public class RespostaEnqueteDtoExample : IExamplesProvider<RespostaEnqueteDto>
    {
        public RespostaEnqueteDto GetExamples()
        {
            return new RespostaEnqueteDto
            {
                Id = 10,
                EnqueteId = 1,
                FuncionarioId = 100,
                Resposta = "Sim"
            };
        }
    }
}