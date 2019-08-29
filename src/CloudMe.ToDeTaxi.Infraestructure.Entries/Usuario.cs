using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Usuario: IdentityUser<Guid>
    {
        public TipoUsuario tipo { get; set; }

        public virtual IEnumerable<UsuarioGrupoUsuario> Grupos { get; set; }
    }
}
