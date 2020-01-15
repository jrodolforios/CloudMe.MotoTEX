using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Passageiro;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Model;
using System.Linq;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class ContatoService : ServiceBase<Contato, ContatoSummary, Guid>, IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public override string GetTag()
        {
            return "contato";
        }

        protected override Task<Contato> CreateEntryAsync(ContatoSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var contato = new Contato
            {
                Id = summary.Id,
                Conteudo = summary.Conteudo,
                Assunto = summary.Assunto,
                Email = summary.Email,
                IdPassageiro = summary.IdPassageiro,
                IdTaxista = summary.IdTaxista,
                Nome = summary.Nome
            };
            return Task.FromResult(contato);
        }

        protected override Task<ContatoSummary> CreateSummaryAsync(Contato entry)
        {
            var contato = new ContatoSummary
            {
                Id = entry.Id,
                Conteudo = entry.Conteudo,
                Assunto = entry.Assunto,
                Email = entry.Email,
                IdPassageiro = entry.IdPassageiro,
                IdTaxista = entry.IdTaxista,
                Nome = entry.Nome
            };

            return Task.FromResult(contato);
        }

        protected override Guid GetKeyFromSummary(ContatoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Contato> GetRepository()
        {
            return _contatoRepository;
        }

        protected override void UpdateEntry(Contato entry, ContatoSummary summary)
        {
            entry.Assunto = summary.Assunto;
            entry.Conteudo = summary.Conteudo;
            entry.Email = summary.Email;
            entry.IdPassageiro = summary.IdPassageiro;
            entry.IdTaxista = summary.IdTaxista;
            entry.Nome = summary.Nome;
        }

        protected override void ValidateSummary(ContatoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Contato: sumário é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Conteudo))
            {
                this.AddNotification(new Notification("Conteúdo", "Contato: Conteúdo do contato é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Assunto))
            {
                this.AddNotification(new Notification("Assunto", "Contato: Assunto do contato é obrigatório"));
            }
        }
    }
}
