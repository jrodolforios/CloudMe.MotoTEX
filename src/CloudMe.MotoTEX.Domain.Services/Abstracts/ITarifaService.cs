using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface ITarifaService : IServiceBase<Tarifa, TarifaSummary, Guid>
    {
        decimal GetValorCorrida(DateTime now, decimal kilometers);
    }
}
