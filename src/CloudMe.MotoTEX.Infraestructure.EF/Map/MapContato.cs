using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapContato : MapBase<Contato, Guid>
    {
        public override void Configure(EntityTypeBuilder<Contato> builder)
        {
            base.Configure(builder);

            builder.ToTable("Contato");

            builder.Property(x => x.Conteudo).IsRequired();
            builder.Property(x => x.Assunto).IsRequired();

            builder.HasOne(x => x.Taxista).WithMany(x => x.Contatos).HasForeignKey(x => x.IdTaxista);
            builder.HasOne(x => x.Passageiro).WithMany(x => x.Contatos).HasForeignKey(x => x.IdPassageiro);
        }
    }
}
