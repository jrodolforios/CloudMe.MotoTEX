using System;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Domain.Model.Foto;

namespace CloudMe.ToDeTaxi.Domain.Model.Passageiro
{
    public class PassageiroSummary
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public string CPF { get; set; }
        public Guid? IdFoto { get; set; }
        public Guid? IdLocalizacaoAtual { get; set; }
        public LocalizacaoSummary Endereco { get; set; } 
    }
}
