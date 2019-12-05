using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using EntityFrameworkCore.Triggers;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CloudMe.ToDeTaxi.Domain.Notifications
{
    public class MonitorGruposUsuarios
    {
        static MonitorGruposUsuarios()
        {
            Triggers<PontoTaxi, CloudMeToDeTaxiContext>.Inserting += async insertingEntry =>
            {
                // novo ponto de taxi..

                // criar grupo de usuários para o novo ponto de táxi
                var grpUsr = new GrupoUsuario()
                {
                    Nome = insertingEntry.Entity.Nome,
                    Descricao = "Grupo de usuários do ponto " + insertingEntry.Entity.Nome
                };

                insertingEntry.Context.Entry(grpUsr).State = EntityState.Added;
                await insertingEntry.Context.SaveChangesAsync();
            };

            Triggers<PontoTaxi, CloudMeToDeTaxiContext>.Deleting += async deletingEntry =>
            {
                // ponto de taxi removido...

                var grpUsr = deletingEntry.Context.GruposUsuario
                    .Include(x => x.Usuarios)
                    .Where(x => x.Nome == deletingEntry.Entity.Nome).FirstOrDefault();

                if (grpUsr != null)
                {
                    // retira os usuários desse grupo
                    foreach (var usr in grpUsr.Usuarios)
                    {
                        deletingEntry.Context.Entry(usr).State = EntityState.Deleted;
                    }

                    // remove o grupo de usuários do ponto de táxi sendo removido
                    deletingEntry.Context.Entry(grpUsr).State = EntityState.Deleted;
                    await deletingEntry.Context.SaveChangesAsync();
                }
            };

            Triggers<Taxista, CloudMeToDeTaxiContext>.Inserting += async insertingEntry =>
            {
                // novo taxista...

                // procura o grupo de taxistas (cria se não existir)
                var grpUsr = insertingEntry.Context.GruposUsuario
                    .Where(x => x.Nome == "Taxistas").FirstOrDefault();

                if (grpUsr == null)
                {
                    grpUsr = new GrupoUsuario()
                    {
                        Nome = "Taxistas",
                        Descricao = "Grupo de usuários taxistas"
                    };

                    insertingEntry.Context.Entry(grpUsr).State = EntityState.Added;
                }

                // inserir no grupo de taxistas
                var usrGrpUsr = new UsuarioGrupoUsuario()
                {
                    IdUsuario = insertingEntry.Entity.Id,
                    IdGrupoUsuario = grpUsr.Id
                };

                insertingEntry.Context.Entry(usrGrpUsr).State = EntityState.Added;
                await insertingEntry.Context.SaveChangesAsync();
            };

            Triggers<Taxista, CloudMeToDeTaxiContext>.Deleting += async deletingEntry =>
            {
                // taxista removido...

                // retira do grupo de taxistas
                var grpUsr = deletingEntry.Context.GruposUsuario
                    .Include(x => x.Usuarios)
                    .Where(x => x.Nome == "Taxistas").FirstOrDefault();
            
                if (grpUsr != null)
                {
                    var usrGrpUsr = grpUsr.Usuarios.FirstOrDefault(x => x.IdUsuario == deletingEntry.Entity.Id);
                    if (usrGrpUsr != null)
                    {
                        deletingEntry.Context.Entry(usrGrpUsr).State = EntityState.Deleted;
                        await deletingEntry.Context.SaveChangesAsync();
                    }
                }
            };

            Triggers<Taxista, CloudMeToDeTaxiContext>.Updating += async updatingEntry =>
            {
                // taxista alterado...

                if (updatingEntry.Original.IdPontoTaxi != updatingEntry.Entity.IdPontoTaxi) // alterou o ponto de taxi
                {
                    // remove do grupo de usuários do ponto de taxi anterior
                    var grpUsrAnt = updatingEntry.Context.GruposUsuario
                        .Include(x => x.Usuarios)
                        .Where(x => x.Nome == "Taxistas").FirstOrDefault();

                    if (grpUsrAnt != null)
                    {
                        var usrGrpUrs = grpUsrAnt.Usuarios.FirstOrDefault(x => x.IdUsuario == updatingEntry.Entity.Id);
                        if (usrGrpUrs != null)
                        {
                            updatingEntry.Context.Entry(usrGrpUrs).State = EntityState.Deleted;
                        }
                    }

                    // adiciona no grupo de usuários do novo ponto de taxi (cria o grupo se não existir)
                    var grpUsr = updatingEntry.Context.GruposUsuario
                        .Where(x => x.Nome == "Taxistas").FirstOrDefault();

                    if (grpUsr == null)
                    {
                        grpUsr = new GrupoUsuario()
                        {
                            Nome = "Taxistas",
                            Descricao = "Grupo de usuários taxistas"
                        };

                        updatingEntry.Context.Entry(grpUsr).State = EntityState.Added;
                    }

                    var usrGrpUsr = new UsuarioGrupoUsuario()
                    {
                        IdUsuario = updatingEntry.Entity.Id,
                        IdGrupoUsuario = grpUsr.Id
                    };

                    updatingEntry.Context.Entry(usrGrpUsr).State = EntityState.Added;
                    await updatingEntry.Context.SaveChangesAsync();
                }
            };

            Triggers<Passageiro, CloudMeToDeTaxiContext>.Inserting += async insertingEntry =>
            {
                // novo passageiro...

                // procura o grupo de passageiros (cria se não existir)
                var grpUsr = insertingEntry.Context.GruposUsuario
                    .Where(x => x.Nome == "Passageiros").FirstOrDefault();

                if (grpUsr == null)
                {
                    grpUsr = new GrupoUsuario()
                    {
                        Nome = "Passageiros",
                        Descricao = "Grupo de usuários passageiros"
                    };

                    insertingEntry.Context.Entry(grpUsr).State = EntityState.Added;
                }

                // inserir no grupo de passageiros
                var usrGrpUsr = new UsuarioGrupoUsuario()
                {
                    IdUsuario = insertingEntry.Entity.Id,
                    IdGrupoUsuario = grpUsr.Id
                };

                insertingEntry.Context.Entry(usrGrpUsr).State = EntityState.Added;
                await insertingEntry.Context.SaveChangesAsync();
            };

            Triggers<Passageiro, CloudMeToDeTaxiContext>.Deleting += async deletingEntry =>
            {
                // retira do grupo de passageiros
                var grpUsr = deletingEntry.Context.GruposUsuario
                    .Include(x => x.Usuarios)
                    .Where(x => x.Nome == "Passageiros").FirstOrDefault();

                if (grpUsr != null)
                {
                    var usrGrpUsr = grpUsr.Usuarios.FirstOrDefault(x => x.IdUsuario == deletingEntry.Entity.Id);
                    if (usrGrpUsr != null)
                    {
                        deletingEntry.Context.Entry(usrGrpUsr).State = EntityState.Deleted;
                        await deletingEntry.Context.SaveChangesAsync();
                    }
                }
            };
        }
    }
}
