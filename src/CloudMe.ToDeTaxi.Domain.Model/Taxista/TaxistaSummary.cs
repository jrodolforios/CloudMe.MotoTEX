using System;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Domain.Model.Foto;

namespace CloudMe.ToDeTaxi.Domain.Model.Taxista
{
    public class TaxistaSummary
    {
        public Guid Id { get; set; }
        public bool Ativo { get; set; }

        public Guid? IdLocalizacaoAtual { get; set; }
        public Guid? IdPontoTaxi { get; set; }

        public UsuarioSummary Usuario { get; set; } 
        public EnderecoSummary Endereco { get; set; }
        public FotoSummary Foto { get; set; }
    }
}
