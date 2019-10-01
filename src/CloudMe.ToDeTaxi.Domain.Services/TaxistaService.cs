using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class TaxistaService : ServiceBase<Taxista, TaxistaSummary, Guid>, ITaxistaService
    {
        private string[] defaultPaths = {"Endereco", "Usuario"};

        private readonly ITaxistaRepository _TaxistaRepository;

        public TaxistaService(
            ITaxistaRepository TaxistaRepository)
        {
            _TaxistaRepository = TaxistaRepository;
        }

        protected override Task<Taxista> CreateEntryAsync(TaxistaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Taxista = new Taxista
            {
                Id = summary.Id,
                IdUsuario = summary.Usuario.Id,
                IdFoto = summary.IdFoto,
                IdLocalizacaoAtual = summary.IdLocalizacaoAtual,
                IdPontoTaxi = summary.IdPontoTaxi,
                IdEndereco = summary.Endereco.Id,
            };
            return Task.FromResult(Taxista);
        }

        protected override Task<TaxistaSummary> CreateSummaryAsync(Taxista entry)
        {
            var Taxista = new TaxistaSummary
            {
                Id = entry.Id,
                IdFoto = entry.IdFoto,
                IdLocalizacaoAtual = entry.IdLocalizacaoAtual,
                IdPontoTaxi = entry.IdPontoTaxi,
                Usuario = new UsuarioSummary()
                {
                    Id = entry.Usuario.Id,
                    Nome = entry.Usuario.Nome,
                    Email = entry.Usuario.Email,
                    Telefone = entry.Usuario.PhoneNumber
                },
                Endereco = new EnderecoSummary()
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
                },
            };

            return Task.FromResult(Taxista);
        }

        protected override Guid GetKeyFromSummary(TaxistaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Taxista> GetRepository()
        {
            return _TaxistaRepository;
        }

        protected override void UpdateEntry(Taxista entry, TaxistaSummary summary)
        {
            entry.IdUsuario = summary.Usuario.Id;
            entry.IdFoto = summary.IdFoto;
            entry.IdLocalizacaoAtual = summary.IdLocalizacaoAtual;
            entry.IdPontoTaxi = summary.IdPontoTaxi;
            entry.IdEndereco = summary.Endereco.Id;
        }

        protected override void ValidateSummary(TaxistaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Taxista: sumário é obrigatório"));
            }
        }

        public override async Task<Taxista> Get(Guid key, string[] paths = null)
        {
            return await base.Get(key, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override async Task<IEnumerable<Taxista>> GetAll(string[] paths = null)
        {
            return await base.GetAll(paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override IEnumerable<Taxista> Search(Expression<Func<Taxista, bool>> where, string[] paths = null, SearchOptions options = null)
        {
            return base.Search(where, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths, options);
        }
    }
}
