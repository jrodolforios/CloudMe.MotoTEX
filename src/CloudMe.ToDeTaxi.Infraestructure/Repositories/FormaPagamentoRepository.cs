﻿using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class FormaPagamentoRepository : BaseRepository<FormaPagamento>, IFormaPagamentoRepository
    {
        public FormaPagamentoRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}
