using System;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;

namespace CloudMe.ToDeTaxi.Domain.Model.Passageiro
{
    public class PassageiroSummary
    {
        public Guid Id { get; set; }
        public Guid? IdFoto { get; set; }
        public Guid? IdLocalizacaoAtual { get; set; }
        public EnderecoSummary Endereco { get; set; } 
        public UsuarioSummary Usuario { get; set; } 
    }
}
