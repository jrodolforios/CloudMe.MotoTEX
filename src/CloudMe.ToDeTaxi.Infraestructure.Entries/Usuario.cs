using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Usuario: IdentityUser<Guid>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public TipoUsuario tipo { get; set; }
        //public DateTime DataRegistro { get; set; }
        //public DateTime DataExclusao { get; set; }
        //public bool Ativo { get; set; }
        public virtual IEnumerable<UsuarioGrupoUsuario> Grupos { get; set; }
    }
}
