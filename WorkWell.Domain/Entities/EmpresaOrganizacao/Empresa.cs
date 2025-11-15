using System.Collections.Generic;

namespace WorkWell.Domain.Entities.EmpresaOrganizacao
{
    public class Empresa
    {
        public long Id { get; set; }
        public string Nome { get; set; } = null!;
        public string EmailAdmin { get; set; } = null!;
        public string SenhaAdmin { get; set; } = null!;
        public string TokenAcesso { get; set; } = null!;
        public string LogoUrl { get; set; } = null!;
        public string CorPrimaria { get; set; } = null!;
        public string CorSecundaria { get; set; } = null!;
        public string Missao { get; set; } = null!;
        public string PoliticaBemEstar { get; set; } = null!;
        public List<Funcionario> Funcionarios { get; set; } = [];
        public List<Setor> Setores { get; set; } = [];
    }
}