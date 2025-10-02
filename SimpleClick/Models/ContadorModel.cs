using System.ComponentModel.DataAnnotations;

namespace ContadorCliques.Models
{
    /// <summary>
    /// Modelo que representa o contador de cliques no banco de dados
    /// </summary>
    public class ContadorModel
    {
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Número total de cliques registrados
        /// </summary>
        public int Cliques { get; set; }
        
        /// <summary>
        /// Data da última atualização do contador
        /// </summary>
        public DateTime UltimaAtualizacao { get; set; }
    }
}