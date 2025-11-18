using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AtividadesBemEstar;
using WorkWell.Domain.Enums.AtividadesBemEstar;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class AtividadeBemEstarDtoExample : IExamplesProvider<AtividadeBemEstarDto>
    {
        public AtividadeBemEstarDto GetExamples()
        {
            return new AtividadeBemEstarDto
            {
                Id = 5,
                EmpresaId = 1,
                Tipo = TipoAtividade.PalestraBemEstar,
                Titulo = "Semana Saúde Mental",
                Descricao = "Palestra sobre saúde mental no ambiente corporativo.",
                DataInicio = DateTime.UtcNow,
                DataFim = DateTime.UtcNow.AddDays(1),
                SetorAlvoId = 2
            };
        }
    }
}