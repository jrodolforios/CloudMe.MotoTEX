using CloudMe.ToDeTaxi.Domain.Enums;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapMensagemDestinatario : MapBase<MensagemDestinatario, Guid>
    {
        public override void Configure(EntityTypeBuilder<MensagemDestinatario> builder)
        {
            base.Configure(builder);

            builder.ToTable("MensagemDestinatario");

            builder.Property(x => x.Status).IsRequired(true).HasDefaultValue(StatusMensagem.Indefinido);

            builder.Property(x => x.DataRecebimento).IsRequired(false);
            builder.Property(x => x.DataLeitura).IsRequired(false);

            builder.HasOne(x => x.Mensagem).WithMany(x => x.Destinatarios).HasForeignKey(x => x.IdMensagem);
            builder.HasOne(x => x.Usuario).WithMany(x => x.MensagensRecebidas).HasForeignKey(x => x.IdUsuario);
            builder.HasOne(x => x.GrupoUsuarios).WithMany(x => x.MensagensRecebidas).HasForeignKey(x => x.IdGrupoUsuario);
        }
    }
}
