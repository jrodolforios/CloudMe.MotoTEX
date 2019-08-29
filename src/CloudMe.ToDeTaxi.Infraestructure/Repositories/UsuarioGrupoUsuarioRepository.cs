﻿using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class UsuarioGrupoUsuarioRepository : BaseRepository<UsuarioGrupoUsuario>, IUsuarioGrupoUsuarioRepository
    {
        public UsuarioGrupoUsuarioRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}
