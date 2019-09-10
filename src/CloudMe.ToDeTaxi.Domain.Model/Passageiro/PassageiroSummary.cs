using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Passageiro
{
    public class PassageiroSummary
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public string CPF { get; set; }
        public Guid IdEndereco { get; set; }
        public Guid? IdLocalizacaoAtual { get; set; }
        public Guid? IdFoto { get; set; }
    }
}
