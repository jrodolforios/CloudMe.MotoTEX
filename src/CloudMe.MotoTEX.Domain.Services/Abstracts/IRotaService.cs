using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Localizacao;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IRotaService : IServiceBase<Rota, RotaSummary, Guid>
    {
    }
}
