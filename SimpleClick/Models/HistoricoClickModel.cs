using System.ComponentModel.DataAnnotations;

namespace ContadorCliques.Models
{
    /// <summary>
    /// Modelo que representa cada clique individual no histórico
    /// </summary>
    public class HistoricoClickModel
    {
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Data e hora exata do clique
        /// </summary>
        public DateTime DataHoraClick { get; set; }
        
        /// <summary>
        /// Número do clique (sequencial)
        /// </summary>
        public int NumeroClick { get; set; }
    }
}