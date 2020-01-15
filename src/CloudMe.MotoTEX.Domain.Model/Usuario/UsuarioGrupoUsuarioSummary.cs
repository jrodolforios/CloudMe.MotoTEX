using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Usuario
{
    public class UsuarioGrupoUsuarioSummary
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdGrupoUsuario { get; set; }
    }
}
