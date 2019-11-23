using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface ICorridaService : IServiceBase<Corrida, CorridaSummary, Guid>
    {
        Task<IEnumerable<CorridaSummary>> GetAllSummariesByPassangerAsync(Guid id);
        Task<IEnumerable<CorridaSummary>> GetAllSummariesByTaxistAsync(Guid id);
        Task<CorridaSummary> GetBySolicitacaoCorrida(Guid id);
        Task<bool> ClassificaTaxista(Guid id, int classificacao);
        Task<bool> ClassificaPassageiro(Guid id, int classificacao);
        Task<int> PausarCorrida(Guid id);
        Task<bool> RetomarCorrida(Guid id);
    }
}
