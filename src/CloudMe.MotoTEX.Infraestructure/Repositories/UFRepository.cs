using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries.Localidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class UFRepository : BaseRepository<UF>, IUFRepository
    {
        public UFRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}
