using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class GrupoUsuario : EntryBase<Guid>
    {
        //public Guid Id { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual IEnumerable<UsuarioGrupoUsuario> Usuarios { get; set; }
    }
}
