using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using CloudMe.MotoTEX.Domain.Enums;
using CloudMe.MotoTEX.Infraestructure.Entries.Localidade;

namespace CloudMe.MotoTEX.Infraestructure.Entries
{
    public class Usuario: IdentityUser<Guid>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public TipoUsuario tipo { get; set; }

        public string DeviceToken { get; set; }
        //public DateTime DataRegistro { get; set; }
        //public DateTime DataExclusao { get; set; }
        //public bool Ativo { get; set; }

        public Guid? IdCidade { get; set; }

        public virtual IEnumerable<UsuarioGrupoUsuario> Grupos { get; set; }

        public virtual IEnumerable<Mensagem> MensagensEnviadas { get; set; }
        
        public virtual IEnumerable<MensagemDestinatario> MensagensRecebidas { get; set; }
        
        public virtual Cidade Cidade { get; set; }
    }
}
