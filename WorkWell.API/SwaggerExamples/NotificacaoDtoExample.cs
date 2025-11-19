using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.Notificacoes;
using WorkWell.Domain.Enums.Notificacoes;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class NotificacaoDtoExample : IExamplesProvider<NotificacaoDto>
    {
        public NotificacaoDto GetExamples()
        {
            return new NotificacaoDto
            {
                FuncionarioId = 20,
                Mensagem = "Você tem uma nova consulta agendada.",
                Tipo = TipoNotificacao.Consulta,
                Lida = false
            };
        }
    }
}