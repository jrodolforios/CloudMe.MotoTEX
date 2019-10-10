using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class EnderecoService : ServiceBase<Endereco, EnderecoSummary, Guid>, IEnderecoService
    {
        private readonly IEnderecoRepository _EnderecoRepository;

        public EnderecoService(IEnderecoRepository EnderecoRepository)
        {
            _EnderecoRepository = EnderecoRepository;
        }

        protected override Task<Endereco> CreateEntryAsync(EnderecoSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Endereco = new Endereco
            {
                Id = summary.Id,
                CEP = summary.CEP,
                Logradouro = summary.Logradouro,
                Numero = summary.Numero,
                Complemento = summary.Complemento,
                Bairro = summary.Bairro,
                Localidade = summary.Localidade,
                UF = summary.UF,
                IdLocalizacao = summary.IdLocalizacao,
            };
            return Task.FromResult(Endereco);
        }

        protected override Task<EnderecoSummary> CreateSummaryAsync(Endereco entry)
        {
            var Endereco = new EnderecoSummary
            {
                Id = entry.Id,
                CEP = entry.CEP,
                Logradouro = entry.Logradouro,
                Numero = entry.Numero,
                Complemento = entry.Complemento,
                Bairro = entry.Bairro,
                Localidade = entry.Localidade,
                UF = entry.UF,
                IdLocalizacao = entry.IdLocalizacao,
            };

            return Task.FromResult(Endereco);
        }

        protected override Guid GetKeyFromSummary(EnderecoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Endereco> GetRepository()
        {
            return _EnderecoRepository;
        }

        protected override void UpdateEntry(Endereco entry, EnderecoSummary summary)
        {
            entry.CEP = summary.CEP;
            entry.Logradouro = summary.Logradouro;
            entry.Numero = summary.Numero;
            entry.Complemento = summary.Complemento;
            entry.Bairro = summary.Bairro;
            entry.Localidade = summary.Localidade;
            entry.UF = summary.UF;
            entry.IdLocalizacao = summary.IdLocalizacao;
        }

        protected override void ValidateSummary(EnderecoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Endereco: sumário é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.CEP))
            {
                this.AddNotification(new Notification("CEP", "Endereço: CEP é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Logradouro))
            {
                this.AddNotification(new Notification("Logradouro", "Endereço: Logradouro é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Numero))
            {
                this.AddNotification(new Notification("Numero", "Endereço: Numero é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Bairro))
            {
                this.AddNotification(new Notification("Bairro", "Endereço: Bairro é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Localidade))
            {
                this.AddNotification(new Notification("Localidade", "Endereço: Localidade é obrigatória"));
            }

            if (string.IsNullOrEmpty(summary.UF))
            {
                this.AddNotification(new Notification("UF", "Endereço: UF é obrigatória"));
            }
        }
    }
}
