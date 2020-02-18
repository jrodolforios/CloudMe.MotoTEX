
namespace CloudMe.MotoTEX.Domain.Model.Corrida
{
    public class EstatisticasCorridas
    {
        public int Total { get; set; }
        public int Agendadas { get; set; }
        public int Solicitadas { get; set; }
        public int EmCurso { get; set; }
        public int EmEspera { get; set; }
        public int Concluidas { get; set; }
        public int CanceladasTaxista { get; set; }
        public int CanceladasPassageiro { get; set; }
        public int EmNegociacao { get; set; }
        public float MediaAvaliacaoTaxista { get; set; }
        public float MediaAvaliacaoPassageiro { get; set; }
    }
}
