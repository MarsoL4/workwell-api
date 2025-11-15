using System;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Enums.OmbudMind;

namespace WorkWell.Domain.Entities.OmbudMind
{
    public class Denuncia
    {
        public long Id { get; set; }
        public long? FuncionarioDenuncianteId { get; set; }
        public Funcionario? Denunciante { get; set; }
        public long EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
        public TipoDenuncia Tipo { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime DataCriacao { get; set; }
        public StatusDenuncia Status { get; set; }
        public string CodigoRastreamento { get; set; } = null!;
    }
}