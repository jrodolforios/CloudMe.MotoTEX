using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Taxista;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IPontoTaxiService : IServiceBase<PontoTaxi, PontoTaxiSummary, Guid>
    {
    }
}
