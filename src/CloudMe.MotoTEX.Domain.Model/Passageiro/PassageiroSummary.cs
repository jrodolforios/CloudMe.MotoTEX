using System;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Domain.Model.Usuario;

namespace CloudMe.MotoTEX.Domain.Model.Passageiro
{
    public class PassageiroSummary
    {
        public Guid Id { get; set; }
        public bool Ativo { get; set; }
        public Guid IdFoto { get; set; }
        public Guid? IdLocalizacaoAtual { get; set; }
        public EnderecoSummary Endereco { get; set; } 
        public UsuarioSummary Usuario { get; set; } 
    }
}
