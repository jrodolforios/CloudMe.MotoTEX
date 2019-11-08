using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class UsuarioGrupoUsuario : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public Guid IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public Guid IdGrupoUsuario { get; set; }
        public virtual GrupoUsuario GrupoUsuario { get; set; }
    }
}
