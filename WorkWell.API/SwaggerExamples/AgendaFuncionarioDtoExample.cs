using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.Agenda;
using System;
using System.Collections.Generic;

namespace WorkWell.API.SwaggerExamples
{
    public class AgendaFuncionarioDtoExample : IExamplesProvider<AgendaFuncionarioDto>
    {
        public AgendaFuncionarioDto GetExamples()
        {
            return new AgendaFuncionarioDto
            {
                Id = 1,
                FuncionarioId = 101,
                Data = DateTime.Today.AddDays(1),
                Itens = new List<ItemAgendaDto>
                {
                    new ItemAgendaDto
                    {
                        Id = 1,
                        AgendaFuncionarioId = 1,
                        Tipo = "consulta",
                        Titulo = "Consulta com Psicólogo",
                        Horario = DateTime.Today.AddDays(1).AddHours(10)
                    }
                }
            };
        }
    }
}