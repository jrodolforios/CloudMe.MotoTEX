using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Foto;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IFotoService : IServiceBase<Foto, FotoSummary, Guid>
    {
    }
}