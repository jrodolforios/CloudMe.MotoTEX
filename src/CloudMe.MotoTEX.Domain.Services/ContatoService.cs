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
        private readonly IPassageiroRepository _passageiroRepository;
        private readonly ITaxistaRepository _taxistaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ContatoService(IContatoRepository contatoRepository, IPassageiroRepository passageiroRepository,
            ITaxistaRepository taxistaRepository, IUsuarioRepository usuarioRepository)
        {
            _contatoRepository = contatoRepository;
            _passageiroRepository = passageiroRepository;
            _taxistaRepository = taxistaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public override string GetTag()
        {
            return "contato";
        }

        protected override Task<Contato> CreateEntryAsync(ContatoSummary summary)
        {
            return Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Contato
                {
                    Id = summary.Id,
                    Conteudo = summary.Conteudo,
                    Assunto = summary.Assunto,
                    Email = summary.Email,
                    IdPassageiro = summary.IdPassageiro,
                    IdTaxista = summary.IdTaxista,
                    Nome = summary.Nome
                };
            });
        }

        protected override async Task<ContatoSummary> CreateSummaryAsync(Contato entry)
        {
            return await Task.Run(async () =>
            {
                if (entry == null) return default;

                if (entry.IdPassageiro.HasValue)
                {
                    var passageiro = (await _passageiroRepository.Search(x => x.Id == entry.IdPassageiro)).FirstOrDefault();
                    var usuario = (await _usuarioRepository.Search(x => x.Id == passageiro.IdUsuario)).FirstOrDefault();

                    entry.Email = usuario.Email;
                    entry.Nome = usuario.Nome;
                }
                else if (entry.IdTaxista.HasValue)
                {
                    var taxista = (await _taxistaRepository.Search(x => x.Id == entry.IdTaxista)).FirstOrDefault();
                    var usuario = (await _usuarioRepository.Search(x => x.Id == taxista.IdUsuario)).FirstOrDefault();

                    entry.Email = usuario.Email;
                    entry.Nome = usuario.Nome;
                }

                return new ContatoSummary
                {
                    Id = entry.Id,
                    Conteudo = entry.Conteudo,
                    Assunto = entry.Assunto,
                    Email = entry.Email,
                    IdPassageiro = entry.IdPassageiro,
                    IdTaxista = entry.IdTaxista,
                    Nome = entry.Nome
                };
            });
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
