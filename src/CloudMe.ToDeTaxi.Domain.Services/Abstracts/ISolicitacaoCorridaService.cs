using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface ISolicitacaoCorridaService : IServiceBase<SolicitacaoCorrida, SolicitacaoCorridaSummary, Guid>
    {
    }
}
