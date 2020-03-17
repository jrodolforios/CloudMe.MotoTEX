using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Localizacao
{
    public class CidadeSummary
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid IdUF { get; set; }
    }
}
