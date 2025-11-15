using System;

namespace WorkWell.Domain.Entities.Agenda
{
    public class ItemAgenda
    {
        public long Id { get; set; }
        public long AgendaFuncionarioId { get; set; }
        public AgendaFuncionario? AgendaFuncionario { get; set; }
        public string Tipo { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public DateTime Horario { get; set; }
    }
}