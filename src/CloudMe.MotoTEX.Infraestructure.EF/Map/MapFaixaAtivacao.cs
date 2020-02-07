using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.EF.Map
{
    class MapFaixaAtivacao : MapBase<FaixaAtivacao, Guid>
    {
        public override void Configure(EntityTypeBuilder<FaixaAtivacao> builder)
        {
            base.Configure(builder);

            builder.ToTable("FaixaAtivacao");

            builder.Property(x => x.Raio).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Janela).IsRequired().HasDefaultValue(0);
        }
    }
}
