using System;
using CloudMe.MotoTEX.Domain.Model.Usuario;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Domain.Model.Foto;

namespace CloudMe.MotoTEX.Domain.Model.Taxista
{
    public class TaxistaSummary
    {
        public Guid Id { get; set; }
        public int? NumeroIdentificacao { get; set; }
        public bool Ativo { get; set; }
        public bool Disponivel { get; set; }

        public Guid IdFoto { get; set; }
        public Guid? IdLocalizacaoAtual { get; set; }
        public Guid? IdPontoTaxi { get; set; }

        public UsuarioSummary Usuario { get; set; } 
        public EnderecoSummary Endereco { get; set; }
    }
}
