using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Passageiro;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IFavoritoService : IServiceBase<Favorito, FavoritoSummary, Guid>
    {
    }
}
