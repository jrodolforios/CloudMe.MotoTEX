using System;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Corrida : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public Guid IdSolicitacao { get; set; }
        public virtual SolicitacaoCorrida Solicitacao { get; set; }

        public Guid IdTaxista { get; set; }
        public virtual Taxista Taxista { get; set; }

        public Guid IdVeiculo { get; set; }
        public virtual Veiculo Veiculo { get; set; }

        public Guid? IdRotaExecutada { get; set; }
        public virtual Rota RotaExecutada { get; set; }

        public Guid IdTarifa { get; set; }
        public virtual Tarifa Tarifa { get; set; }

        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }

        public AvaliacaoUsuario? AvaliacaoTaxista { get; set; }
        public AvaliacaoUsuario? AvaliacaoPassageiro { get; set; }

        public StatusCorrida Status { get; set; }

        public int TempoEmEspera { get; set; }
    }
}
