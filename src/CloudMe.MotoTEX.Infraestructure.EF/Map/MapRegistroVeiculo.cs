using CloudMe.MotoTEX.Infraestructure.Entries.Piloto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapRegistroVeiculo : MapBase<RegistroVeiculo, Guid>
    {
        public override void Configure(EntityTypeBuilder<RegistroVeiculo> builder)
        {
            base.Configure(builder);

            builder.ToTable("RegistroVeiculo");

            builder.Property(x => x.AnoExercicio).IsRequired();
            builder.Property(x => x.Chassi).IsRequired();
            builder.Property(x => x.Renavam).IsRequired();

            builder.HasOne(x => x.Veiculo).WithOne(x => x.RegistroVigente).HasForeignKey<RegistroVeiculo>(x => x.IdVeiculo);
            builder.HasOne(x => x.FotoCRLV).WithOne().HasForeignKey<RegistroVeiculo>(x => x.IdFotoCRLV);
            builder.HasOne(x => x.FotoFrente).WithOne().HasForeignKey<RegistroVeiculo>(x => x.IdFotoFrente);
            builder.HasOne(x => x.FotoTras).WithOne().HasForeignKey<RegistroVeiculo>(x => x.IdFotoTras);
            builder.HasOne(x => x.FotoEsquerda).WithOne().HasForeignKey<RegistroVeiculo>(x => x.IdFotoEsquerda);
            builder.HasOne(x => x.FotoDireita).WithOne().HasForeignKey<RegistroVeiculo>(x => x.IdFotoDireita);
            builder.HasOne(x => x.UF).WithMany().HasForeignKey(x => x.IdUF);
        }
    }
}
