using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries.Piloto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class RegistroVeiculoRepository : BaseRepository<RegistroVeiculo>, IRegistroVeiculoRepository
    {
        public RegistroVeiculoRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}
