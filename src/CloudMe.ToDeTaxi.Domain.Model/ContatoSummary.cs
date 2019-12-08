using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model
{
    public class ContatoSummary
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }

        public Guid? IdTaxista { get; set; }

        public Guid? IdPassageiro { get; set; }
    }
}
