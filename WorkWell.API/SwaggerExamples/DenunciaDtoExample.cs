using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.OmbudMind;
using WorkWell.Domain.Enums.OmbudMind;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class DenunciaDtoExample : IExamplesProvider<DenunciaDto>
    {
        public DenunciaDto GetExamples()
        {
            return new DenunciaDto
            {
                FuncionarioDenuncianteId = null,
                EmpresaId = 1,
                Tipo = TipoDenuncia.AssedioMoral,
                Descricao = "Fui vítima de assédio moral pelo gestor.",
                DataCriacao = DateTime.UtcNow,
                Status = StatusDenuncia.Aberta,
                CodigoRastreamento = "9CFD103BF9B7"
            };
        }
    }
}