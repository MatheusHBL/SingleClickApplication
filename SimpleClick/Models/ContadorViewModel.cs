namespace ContadorCliques.Models
{
    /// <summary>
    /// ViewModel que combina o contador e o histórico para exibição na View
    /// </summary>
    public class ContadorViewModel
    {
        public ContadorModel Contador { get; set; }
        public List<HistoricoClickModel> Historico { get; set; }
    }
}