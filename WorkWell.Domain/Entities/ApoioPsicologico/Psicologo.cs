using System.Collections.Generic;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.ApoioPsicologico
{
    public class Psicologo : Funcionario
    {
        public string Crp { get; set; } = null!;
        public List<ConsultaPsicologica> Atendimentos { get; set; } = [];
    }
}