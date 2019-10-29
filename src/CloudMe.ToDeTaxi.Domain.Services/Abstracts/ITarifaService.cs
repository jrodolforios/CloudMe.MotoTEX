using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface ITarifaService : IServiceBase<Tarifa, TarifaSummary, Guid>
    {
        decimal GetValorKmRodadoAtual(DateTime now);
    }
}
