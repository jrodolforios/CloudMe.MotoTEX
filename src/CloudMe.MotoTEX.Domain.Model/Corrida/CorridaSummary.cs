using System;
using CloudMe.MotoTEX.Domain.Enums;

namespace CloudMe.MotoTEX.Domain.Model.Corrida
{
    public class CorridaSummary
    {
        public Guid Id { get; set; }
        public Guid IdSolicitacao { get; set; }
        public Guid IdTaxista { get; set; }
        public Guid IdVeiculo { get; set; }
        public Guid? IdRotaExecutada { get; set; }
        public Guid IdTarifa { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public AvaliacaoUsuario? AvaliacaoTaxista { get; set; }
        public AvaliacaoUsuario? AvaliacaoPassageiro { get; set; }
        public StatusCorrida Status { get; set; }
        public int TempoEmEspera { get; set; }
    }
}
