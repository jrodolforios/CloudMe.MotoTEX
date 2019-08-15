using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Call;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface ICallService : IServiceBase<Call, CallSummary, Guid>
    {
    }
}
