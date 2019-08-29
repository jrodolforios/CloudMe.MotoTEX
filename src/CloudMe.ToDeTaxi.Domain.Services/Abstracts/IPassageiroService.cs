using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IPassageiroService : IServiceBase<Passageiro, PassageiroSummary, Guid>
    {
    }
}
