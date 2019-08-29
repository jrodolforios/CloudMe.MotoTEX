using System;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Passageiro : EntryBase
    {
        public Guid Id { get; set; }

        public Guid IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public string CPF { get; set; }

        public Guid IdEndereco { get; set; }
        public virtual Localizacao Endereco { get; set; }

        public Guid? IdLocalizacaoAtual { get; set; }
        public virtual Localizacao LocalizacaoAtual { get; set; }

        public byte[] Foto { get; set; }

        public virtual IEnumerable<Favorito> TaxistasFavoritos { get; set; }
        public virtual IEnumerable<SolicitacaoCorrida> SolicitacoesCorrida { get; set; }

    }
}
