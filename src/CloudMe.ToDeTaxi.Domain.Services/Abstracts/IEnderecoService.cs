using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IEnderecoService : IServiceBase<Endereco, EnderecoSummary, Guid>
    {
    }
}
