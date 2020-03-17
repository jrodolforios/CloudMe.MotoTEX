using CloudMe.MotoTEX.Infraestructure.Entries.Localidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    public class MapCidade : MapBase<Cidade, Guid>
    {
        public override void Configure(EntityTypeBuilder<Cidade> builder)
        {
            base.Configure(builder);

            builder.ToTable("UF");
            builder.Property(x => x.Nome).IsRequired();
            builder.HasOne(x => x.UF).WithMany(x => x.Cidades).HasForeignKey(x => x.IdUF);
        }
    }
}
