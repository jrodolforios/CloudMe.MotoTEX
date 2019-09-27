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
        //private readonly IUsuarioService _usuarioService;
        //private readonly ILocalizacaoService _localizacaoService;

        public TaxistaService(
            ITaxistaRepository TaxistaRepository)
            //IUsuarioService usuarioService,
            //ILocalizacaoService localizacaoService)
        {
            _TaxistaRepository = TaxistaRepository;
            //_usuarioService = usuarioService;
            //_localizacaoService = localizacaoService;
        }

        protected override Task<Taxista> CreateEntryAsync(TaxistaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Taxista = new Taxista
            {
                Id = summary.Id,
                IdUsuario = summary.Usuario.Id,
                RG = summary.RG,
                CPF = summary.CPF,
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
                RG = entry.RG,
                CPF = entry.CPF,
                IdFoto = entry.IdFoto,
                IdLocalizacaoAtual = entry.IdLocalizacaoAtual,
                IdPontoTaxi = entry.IdPontoTaxi,
                Usuario = new UsuarioSummary()
                {
                    Id = entry.Usuario.Id,
                    Nome = entry.Usuario.UserName,
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
            entry.RG = summary.RG;
            entry.CPF = summary.CPF;
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

            if (string.IsNullOrEmpty(summary.RG))
            {
                this.AddNotification(new Notification("RG", "Passageiro: RG é obrigatório"));
            }

            if (string.IsNullOrEmpty(summary.CPF))
            {
                this.AddNotification(new Notification("CPF", "Passageiro: CPF é obrigatório"));
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

        /*public override async Task<Taxista> CreateAsync(TaxistaSummary summary)
        {
            ValidateSummary(summary);

            if (IsInvalid() || _usuarioService.IsInvalid() || _localizacaoService.IsInvalid())
            {
                return null;
            }

            // cria um usuario para o taxista
            var usuario = _usuarioService.CreateAsync(summary.Usuario);
            if(usuario == null)
            {
                if(_usuarioService.IsInvalid())
                {
                    this.AddNotifications(_usuarioService.Notifications);
                }
            }

            // cria um endereço para o taxista
            var endereco = _localizacaoService.CreateAsync(summary.Endereco);
            if(endereco == null)
            {
                if(_localizacaoService.IsInvalid())
                {
                    this.AddNotifications(_localizacaoService.Notifications);
                }
            }

            var entry = await CreateEntryAsync(summary);

            if (IsInvalid())
            {
                return null;
            }

            if (await GetRepository().SaveAsync(entry))
            {
                return entry;
            }
            else
            {
                this.AddNotifications(GetRepository().Notifications);
            }

            return null;
        }*/

        /*public override async Task<Taxista> UpdateAsync(TaxistaSummary summary)
        {
            ValidateSummary(summary);

            if (IsInvalid())
            {
                return null;
            }

            var taxista = await GetRepository().FindByIdAsync(GetKeyFromSummary(summary));
            if(taxista != null)
            {
                UpdateEntry(taxista, summary);

                // atualiza tambem o usuario do taxista
                await _usuarioService.UpdateAsync(summary.Usuario);

                if (IsInvalid())
                {
                    return null;
                }

                if (await GetRepository().ModifyAsync(taxista))
                {
                    return taxista;
                }
            }

            return null;
        }*/

        /*public override async Task<bool> DeleteAsync(Guid key)
        {
            var repo = GetRepository();
            var taxista = await repo.FindByIdAsync(key);

            // remove o usuario associado ao taxista
            if(await this._usuarioService.DeleteAsync(taxista.Usuario.Id))
            {
                return await repo.DeleteAsync(taxista);
            }

            return false;
        }*/
    }
}
