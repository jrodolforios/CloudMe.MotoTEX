﻿using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Infraestructure.Entries.Localidade;
using System;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface ICidadeService : IServiceBase<Cidade, CidadeSummary, Guid>
    {
    }
}