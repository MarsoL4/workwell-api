using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.ApoioPsicologico;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class ChatAnonimoDtoExample : IExamplesProvider<ChatAnonimoDto>
    {
        public ChatAnonimoDto GetExamples()
        {
            return new ChatAnonimoDto
            {
                RemetenteId = 100,
                PsicologoId = 201,
                Mensagem = "Estou passando por dificuldades e gostaria de conversar.",
                DataEnvio = DateTime.UtcNow,
                Anonimo = true
            };
        }
    }
}