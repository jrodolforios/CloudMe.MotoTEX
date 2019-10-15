using System;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;

namespace CloudMe.ToDeTaxi.Domain.Model.Taxista
{
    public class TaxistaSummary
    {
        public Guid Id { get; set; }
        public bool Ativo { get; set; }

        public Guid? IdFoto { get; set; }
        public Guid? IdLocalizacaoAtual { get; set; }
        public Guid? IdPontoTaxi { get; set; }

        public UsuarioSummary Usuario { get; set; } 
        public EnderecoSummary Endereco { get; set; } 
    }
}
