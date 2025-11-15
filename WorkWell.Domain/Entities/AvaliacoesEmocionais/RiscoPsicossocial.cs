using System;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.AvaliacoesEmocionais
{
    public class RiscoPsicossocial
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }
        public string Categoria { get; set; } = null!;
        public int NivelRisco { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}