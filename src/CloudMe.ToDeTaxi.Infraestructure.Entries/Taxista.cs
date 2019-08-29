using System;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Taxista : EntryBase
    {
        public Guid Id { get; set; }

        public Guid IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public Guid IdEndereco { get; set; }
        public virtual Localizacao Endereco { get; set; }

        public Guid? IdLocalizacaoAtual { get; set; }
        public virtual Localizacao LocalizacaoAtual { get; set; }

        public byte[] Foto { get; set; }

        public virtual IEnumerable<Corrida> Corridas { get; set; }
        public virtual IEnumerable<VeiculoTaxista> Veiculos { get; set; }
        public virtual IEnumerable<FaixaDescontoTaxista> FaixasDesconto { get; set; }
        public virtual IEnumerable<FormaPagamentoTaxista> FormasPagamento { get; set; }

    }
}
