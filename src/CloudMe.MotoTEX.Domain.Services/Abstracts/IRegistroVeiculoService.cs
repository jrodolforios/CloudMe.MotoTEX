using CloudMe.MotoTEX.Domain.Model.Veiculo;
using CloudMe.MotoTEX.Infraestructure.Entries.Piloto;
using System;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IRegistroVeiculoService : IServiceBase<RegistroVeiculo, RegistroVeiculoSummary, Guid>
    {
    }
}
