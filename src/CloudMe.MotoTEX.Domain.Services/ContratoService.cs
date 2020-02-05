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
    public class ContratoService : ServiceBase<Contrato, ContratoSummary, Guid>, IContratoService
    {
        private readonly IContratoRepository _contratoRepository;

        public ContratoService(IContratoRepository FavoritoRepository)
        {
            _contratoRepository = FavoritoRepository;
        }

        public override string GetTag()
        {
            return "contrato";
        }

        public async Task<ContratoSummary> UltimoContratoValido()
        {
            var encontrado = new ContratoSummary();
            return await CreateSummaryAsync((await _contratoRepository.Search(x => x.UltimaVersao)).FirstOrDefault());
        }

        protected override Task<Contrato> CreateEntryAsync(ContratoSummary summary)
        {
            return Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Contrato
                {
                    Id = summary.Id,
                    Conteudo = summary.Conteudo,
                    UltimaVersao = summary.UltimaVersao
                };
            });
        }

        protected override async Task<ContratoSummary> CreateSummaryAsync(Contrato entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;

                return new ContratoSummary
                {
                    Id = entry.Id,
                    Conteudo = entry.Conteudo,
                    UltimaVersao = entry.UltimaVersao
                };
            });
        }

        protected override Guid GetKeyFromSummary(ContratoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Contrato> GetRepository()
        {
            return _contratoRepository;
        }

        protected override void UpdateEntry(Contrato entry, ContratoSummary summary)
        {
            entry.UltimaVersao = summary.UltimaVersao;
            entry.Conteudo = summary.Conteudo;
        }

        protected override void ValidateSummary(ContratoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Contrato: sumário é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.Conteudo))
            {
                this.AddNotification(new Notification("IdPassageiro", "Contrato: Conteúdo do contrato é obrigatório"));
            }
        }
    }
}
