using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Entities.AtividadesBemEstar;
using WorkWell.Domain.Entities.Enquetes;
using WorkWell.Domain.Entities.Indicadores;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Entities.Notificacoes;
using WorkWell.Domain.Entities.OmbudMind;
using WorkWell.Domain.Entities.Agenda;

namespace WorkWell.Infrastructure.Persistence
{
    public class WorkWellDbContext : DbContext
    {
        public WorkWellDbContext(DbContextOptions<WorkWellDbContext> options) : base(options) { }

        // Empresa & organização
        public DbSet<Empresa> Empresas => Set<Empresa>();
        public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
        public DbSet<Setor> Setores => Set<Setor>();

        // Apoio Psicológico
        public DbSet<Psicologo> Psicologos => Set<Psicologo>();
        public DbSet<ConsultaPsicologica> ConsultasPsicologicas => Set<ConsultaPsicologica>();
        public DbSet<ChatAnonimo> ChatsAnonimos => Set<ChatAnonimo>();
        public DbSet<SOSemergencia> SOSemergencias => Set<SOSemergencia>();

        // Atividades de Bem-Estar
        public DbSet<AtividadeBemEstar> AtividadesBemEstar => Set<AtividadeBemEstar>();
        public DbSet<ParticipacaoAtividade> ParticipacoesAtividade => Set<ParticipacaoAtividade>();

        // Enquetes
        public DbSet<Enquete> Enquetes => Set<Enquete>();
        public DbSet<RespostaEnquete> RespostasEnquete => Set<RespostaEnquete>();

        // Indicadores
        public DbSet<IndicadoresEmpresa> IndicadoresEmpresa => Set<IndicadoresEmpresa>();

        // Avaliações emocionais
        public DbSet<AvaliacaoProfunda> AvaliacoesProfundas => Set<AvaliacaoProfunda>();
        public DbSet<MoodCheck> MoodChecks => Set<MoodCheck>();
        public DbSet<RiscoPsicossocial> RiscosPsicossociais => Set<RiscoPsicossocial>();
        public DbSet<PerfilEmocional> PerfisEmocionais => Set<PerfilEmocional>();

        // Notificações
        public DbSet<Notificacao> Notificacoes => Set<Notificacao>();

        // OmbudMind
        public DbSet<Denuncia> Denuncias => Set<Denuncia>();
        public DbSet<InvestigacaoDenuncia> InvestigacoesDenuncia => Set<InvestigacaoDenuncia>();

        // Agenda
        public DbSet<AgendaFuncionario> AgendasFuncionarios => Set<AgendaFuncionario>();
        public DbSet<ItemAgenda> ItensAgenda => Set<ItemAgenda>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Corrige mapeamento de bool para Oracle: NUMBER(1)
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var boolProperties = entity.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(bool) || p.PropertyType == typeof(bool?));
                foreach (var prop in boolProperties)
                {
                    modelBuilder.Entity(entity.ClrType)
                        .Property(prop.Name)
                        .HasConversion<int>()
                        .HasColumnType("NUMBER(1)");
                }
            }
        }
    }
}