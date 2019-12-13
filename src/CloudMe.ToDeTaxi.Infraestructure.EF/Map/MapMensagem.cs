using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Map
{
    class MapMensagem : MapBase<Mensagem, Guid>
    {
        public override void Configure(EntityTypeBuilder<Mensagem> builder)
        {
            base.Configure(builder);

            builder.ToTable("Mensagem");

            builder.Property(x => x.Assunto).IsRequired(false);
            builder.Property(x => x.Corpo).IsRequired(true);

            builder.Property(x => x.Apagada).HasDefaultValue(false);

            builder.HasOne(x => x.Remetente).WithMany(x => x.MensagensEnviadas).HasForeignKey(x => x.IdRemetente);
        }
    }
}
