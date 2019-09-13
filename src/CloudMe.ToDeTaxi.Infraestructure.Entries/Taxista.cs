using System;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Taxista : EntryBase
    {
        public Guid Id { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }

        public Guid IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public Guid IdEndereco { get; set; }
        public virtual Localizacao Endereco { get; set; }

        public Guid? IdLocalizacaoAtual { get; set; }
        public virtual Localizacao LocalizacaoAtual { get; set; }

        public Guid? IdFoto { get; set; }
        public virtual Foto Foto { get; set; }

        public Guid? IdPontoTaxi { get; set; }
        public virtual PontoTaxi PontoTaxi { get; set; }

        public virtual IEnumerable<Corrida> Corridas { get; set; }
        public virtual IEnumerable<VeiculoTaxista> Veiculos { get; set; }
        public virtual IEnumerable<FaixaDescontoTaxista> FaixasDesconto { get; set; }
        public virtual IEnumerable<FormaPagamentoTaxista> FormasPagamento { get; set; }

    }
}
