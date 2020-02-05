using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Domain.Model;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class PontoTaxiService : ServiceBase<PontoTaxi, PontoTaxiSummary, Guid>, IPontoTaxiService
    {
        private string[] defaultPaths = {"Endereco"};

        private readonly IPontoTaxiRepository _PontoTaxiRepository;
        private readonly IGrupoUsuarioRepository _GrupoUsuarioRepository;
        private readonly IUsuarioGrupoUsuarioRepository _UsuarioGrupoUsuarioRepository;

        public PontoTaxiService(
            IPontoTaxiRepository PontoTaxiRepository,
            IGrupoUsuarioRepository GrupoUsuarioRepository,
            IUsuarioGrupoUsuarioRepository UsuarioGrupoUsuarioRepository)
        {
            _PontoTaxiRepository = PontoTaxiRepository;
            _GrupoUsuarioRepository = GrupoUsuarioRepository;
            _UsuarioGrupoUsuarioRepository = UsuarioGrupoUsuarioRepository;
        }

        public override string GetTag()
        {
            return "ponto_taxi";
        }

        protected override async Task<PontoTaxi> CreateEntryAsync(PontoTaxiSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new PontoTaxi
                {
                    Id = summary.Id,
                    Nome = summary.Nome,
                    IdEndereco = summary.Endereco.Id,
                };
            });
        }

        protected override async Task<PontoTaxiSummary> CreateSummaryAsync(PontoTaxi entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;

                var PontoTaxi = new PontoTaxiSummary
                {
                    Id = entry.Id,
                    Nome = entry.Nome
                };

                if (entry.Endereco != null)
                {
                    PontoTaxi.Endereco = new EnderecoSummary()
                    {
                        Id = entry.Endereco.Id,
                        CEP = entry.Endereco.CEP,
                        Logradouro = entry.Endereco.Logradouro,
                        Numero = entry.Endereco.Numero,
                        Complemento = entry.Endereco.Complemento,
                        Bairro = entry.Endereco.Bairro,
                        Localidade = entry.Endereco.Localidade,
                        UF = entry.Endereco.UF,
                        IdLocalizacao = entry.Endereco.IdLocalizacao
                    };
                }

                return PontoTaxi;
            });
        }

        protected override Guid GetKeyFromSummary(PontoTaxiSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<PontoTaxi> GetRepository()
        {
            return _PontoTaxiRepository;
        }

        protected override void UpdateEntry(PontoTaxi entry, PontoTaxiSummary summary)
        {
            entry.Nome = summary.Nome;
            entry.IdEndereco = summary.Endereco.Id;
        }

        protected override void ValidateSummary(PontoTaxiSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "PontoTaxi: sumário é obrigatório"));
            }

            if (String.IsNullOrEmpty(summary.Nome))
            {
                this.AddNotification(new Notification("Nome", "PontoTaxi: nome não fornecido"));
            }
        }

        public override async Task<PontoTaxi> Get(Guid key, string[] paths = null)
        {
            return await base.Get(key, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override async Task<IEnumerable<PontoTaxi>> GetAll(string[] paths = null)
        {
            return await base.GetAll(paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override Task<IEnumerable<PontoTaxi>> Search(Expression<Func<PontoTaxi, bool>> where, string[] paths = null, Pagination options = null)
        {
            return base.Search(where, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths, options);
        }
    }
}
