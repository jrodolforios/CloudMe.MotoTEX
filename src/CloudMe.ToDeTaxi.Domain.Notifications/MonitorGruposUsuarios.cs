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
        public MonitorGruposUsuarios() { }
   
        static MonitorGruposUsuarios()
        {
            Triggers<PontoTaxi, CloudMeToDeTaxiContext>.Inserting += insertingEntry =>
            {
                // novo ponto de taxi..

                // criar grupo de usuários para o novo ponto de táxi
                var grpUsr = new GrupoUsuario()
                {
                    Nome = insertingEntry.Entity.Nome,
                    Descricao = "Grupo de usuários do ponto " + insertingEntry.Entity.Nome
                };

                insertingEntry.Context.Entry(grpUsr).State = EntityState.Added;
            };

            Triggers<PontoTaxi, CloudMeToDeTaxiContext>.Deleting += deletingEntry =>
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
                }
            };

            Triggers<Taxista, CloudMeToDeTaxiContext>.Inserting += insertingEntry =>
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
                    IdUsuario = insertingEntry.Entity.IdUsuario.Value,
                    IdGrupoUsuario = grpUsr.Id
                };

                insertingEntry.Context.Entry(usrGrpUsr).State = EntityState.Added;
            };

            Triggers<Taxista, CloudMeToDeTaxiContext>.Deleting += deletingEntry =>
            {
                // taxista removido...

                // retira do grupo de taxistas
                var grpUsr = deletingEntry.Context.GruposUsuario
                    .Include(x => x.Usuarios)
                    .Where(x => x.Nome == "Taxistas").FirstOrDefault();
            
                if (grpUsr != null)
                {
                    var usrGrpUsr = grpUsr.Usuarios.FirstOrDefault(x => x.IdUsuario == deletingEntry.Entity.IdUsuario.Value);
                    if (usrGrpUsr != null)
                    {
                        deletingEntry.Context.Entry(usrGrpUsr).State = EntityState.Deleted;
                    }
                }
            };

            Triggers<Taxista, CloudMeToDeTaxiContext>.Updating += updatingEntry =>
            {
                // taxista alterado...

                if (updatingEntry.Original.IdPontoTaxi != updatingEntry.Entity.IdPontoTaxi) // alterou o ponto de taxi
                {
                    var ptTaxiAnterior = updatingEntry.Context.PontosTaxi.Where(x => x.Id == updatingEntry.Original.IdPontoTaxi).FirstOrDefault();
                    var ptTaxiAtual = updatingEntry.Context.PontosTaxi.Where(x => x.Id == updatingEntry.Entity.IdPontoTaxi).FirstOrDefault();

                    if (ptTaxiAnterior != null)
                    {
                        // remove do grupo de usuários do ponto de taxi anterior
                        var grpUsrAnt = updatingEntry.Context.GruposUsuario
                            .Include(x => x.Usuarios)
                            .Where(x => x.Nome == ptTaxiAnterior.Nome).FirstOrDefault();

                        if (grpUsrAnt != null)
                        {
                            var usrGrpUrs = grpUsrAnt.Usuarios.FirstOrDefault(x => x.IdUsuario == updatingEntry.Entity.IdUsuario.Value);
                            if (usrGrpUrs != null)
                            {
                                updatingEntry.Context.Entry(usrGrpUrs).State = EntityState.Deleted;
                            }
                        }
                    }

                    if (ptTaxiAtual != null)
                    {
                        // adiciona no grupo de usuários do novo ponto de taxi (se houver)
                        var grpUsr = updatingEntry.Context.GruposUsuario
                            .Where(x => x.Nome == ptTaxiAtual.Nome).FirstOrDefault();

                        if (grpUsr != null)
                        {
                            var usrGrpUsr = new UsuarioGrupoUsuario()
                            {
                                IdUsuario = updatingEntry.Entity.IdUsuario.Value,
                                IdGrupoUsuario = grpUsr.Id
                            };

                            updatingEntry.Context.Entry(usrGrpUsr).State = EntityState.Added;
                        }
                    }
                }
            };

            Triggers<Passageiro, CloudMeToDeTaxiContext>.Inserting += insertingEntry =>
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
                    IdUsuario = insertingEntry.Entity.IdUsuario.Value,
                    IdGrupoUsuario = grpUsr.Id
                };

                insertingEntry.Context.Entry(usrGrpUsr).State = EntityState.Added;
            };

            Triggers<Passageiro, CloudMeToDeTaxiContext>.Deleting += deletingEntry =>
            {
                // retira do grupo de passageiros
                var grpUsr = deletingEntry.Context.GruposUsuario
                    .Include(x => x.Usuarios)
                    .Where(x => x.Nome == "Passageiros").FirstOrDefault();

                if (grpUsr != null)
                {
                    var usrGrpUsr = grpUsr.Usuarios.FirstOrDefault(x => x.IdUsuario == deletingEntry.Entity.IdUsuario.Value);
                    if (usrGrpUsr != null)
                    {
                        deletingEntry.Context.Entry(usrGrpUsr).State = EntityState.Deleted;
                    }
                }
            };
        }
    }
}
