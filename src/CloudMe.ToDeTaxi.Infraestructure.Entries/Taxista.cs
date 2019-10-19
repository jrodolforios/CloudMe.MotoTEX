using System;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Taxista : EntryBase
    {
        public Guid Id { get; set; }
        public bool Ativo { get; set; }

        public Guid? IdUsuario { get; set; } // relaxa o relacionamento com o usuario pois este (usuário) é gerenciado externamente (Identity server)
        public Usuario Usuario { get; set; }

        public Guid IdEndereco { get; set; }
        public virtual Endereco Endereco { get; set; }

        public Guid? IdLocalizacaoAtual { get; set; }
        public virtual Localizacao LocalizacaoAtual { get; set; }

        public Guid IdFoto { get; set; }
        public virtual Foto Foto { get; set; }

        public Guid? IdPontoTaxi { get; set; }
        public virtual PontoTaxi PontoTaxi { get; set; }

        public virtual IEnumerable<Corrida> Corridas { get; set; }
        public virtual IEnumerable<VeiculoTaxista> Veiculos { get; set; }
        public virtual IEnumerable<FaixaDescontoTaxista> FaixasDesconto { get; set; }
        public virtual IEnumerable<FormaPagamentoTaxista> FormasPagamento { get; set; }

    }
}
