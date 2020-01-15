using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Foto;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IFotoService : IServiceBase<Foto, FotoSummary, Guid>
    {
    }
}
