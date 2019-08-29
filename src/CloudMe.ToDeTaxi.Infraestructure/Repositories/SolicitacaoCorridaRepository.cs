﻿using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class SolicitacaoCorridaRepository : BaseRepository<SolicitacaoCorrida>, ISolicitacaoCorridaRepository
    {
        public SolicitacaoCorridaRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}