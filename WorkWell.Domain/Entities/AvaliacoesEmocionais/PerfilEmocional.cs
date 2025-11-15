using System;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.AvaliacoesEmocionais
{
    public class PerfilEmocional
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }
        public string HumorInicial { get; set; } = null!;
        public string Rotina { get; set; } = null!;
        public string PrincipaisEstressores { get; set; } = null!;
        public DateTime DataCriacao { get; set; }
    }
}