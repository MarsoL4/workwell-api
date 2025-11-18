using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.Agenda;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class ItemAgendaDtoExample : IExamplesProvider<ItemAgendaDto>
    {
        public ItemAgendaDto GetExamples()
        {
            return new ItemAgendaDto
            {
                Id = 1,
                AgendaFuncionarioId = 1,
                Tipo = "atividade",
                Titulo = "Palestra sobre Resiliência",
                Horario = DateTime.Today.AddDays(2).AddHours(15)
            };
        }
    }
}