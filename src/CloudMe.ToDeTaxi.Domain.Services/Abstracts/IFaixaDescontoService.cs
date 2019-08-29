﻿using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IFaixaDescontoService : IServiceBase<FaixaDesconto, FaixaDescontoSummary, Guid>
    {
    }
}