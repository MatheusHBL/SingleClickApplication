using ContadorCliques.Models;
using Microsoft.EntityFrameworkCore;

namespace ContadorCliques.Data
{
    /// <summary>
    /// Contexto do banco de dados para o Entity Framework Core
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Tabela de contadores
        /// </summary>
        public DbSet<ContadorModel> Contadores { get; set; }
        
        /// <summary>
        /// Tabela de histórico de cliques
        /// </summary>
        public DbSet<HistoricoClickModel> HistoricoClicks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data - cria um contador inicial com valor zero
            modelBuilder.Entity<ContadorModel>().HasData(
                new ContadorModel
                {
                    Id = 1,
                    Cliques = 0,
                    UltimaAtualizacao = DateTime.Now
                }
            );
        }
    }
}