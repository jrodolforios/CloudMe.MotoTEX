using CloudMe.ToDeTaxi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class SolicitacaoCorridaTaxista : EntryBase<Guid>
    {
        public Guid IdSolicitacaoCorrida { get; set; }
        public virtual SolicitacaoCorrida SolicitacaoCorrida { get; set; }

        public Guid IdTaxista { get; set; }
        public virtual Taxista Taxista { get; set; }

        public AcaoTaxistaSolicitacaoCorrida Acao { get; set; }
    }
}
