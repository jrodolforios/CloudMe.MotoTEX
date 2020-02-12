using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Domain.Model.Usuario;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CloudMe.MotoTEX.Domain.Model.Foto;
using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using System.Globalization;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class TaxistaService : ServiceBase<Taxista, TaxistaSummary, Guid>, ITaxistaService
    {
        private string[] defaultPaths = { "Endereco", "Usuario", "Foto", "LocalizacaoAtual" };

        private readonly ITaxistaRepository _TaxistaRepository;
        private readonly IFotoService _FotoService;
        private readonly IVeiculoTaxistaService _veiculoTaxistaService;
        private readonly ILocalizacaoService _LocalizacaoService;
        private readonly ICorridaRepository _corridaRepository;
        private readonly IProxyNotificacoesLocalizacao _proxyNotificacoesLocalizacao;

        public TaxistaService(
            ITaxistaRepository TaxistaRepository,
            IFotoService FotoService,
            IVeiculoTaxistaService veiculoTaxistaService,
            ILocalizacaoService LocalizacaoService,
            ICorridaRepository corridaRepository,
            IProxyNotificacoesLocalizacao proxyNotificacoesLocalizacao
            )
        {
            _TaxistaRepository = TaxistaRepository;
            _FotoService = FotoService;
            _veiculoTaxistaService = veiculoTaxistaService;
            _LocalizacaoService = LocalizacaoService;
            _corridaRepository = corridaRepository;
            _proxyNotificacoesLocalizacao = proxyNotificacoesLocalizacao;
        }

        public override string GetTag()
        {
            return "taxista";
        }

        protected override async Task<Taxista> CreateEntryAsync(TaxistaSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Taxista
                {
                    Id = summary.Id,
	                NumeroIdentificacao = summary.NumeroIdentificacao,
                    Ativo = summary.Ativo,
                    IdUsuario = summary.Usuario.Id,
                    IdFoto = summary.IdFoto,
                    IdLocalizacaoAtual = summary.IdLocalizacaoAtual,
                    IdPontoTaxi = summary.IdPontoTaxi,
                    IdEndereco = summary.Endereco.Id,
                };
            });
        }

        public async override Task<Taxista> CreateAsync(TaxistaSummary summary)
        {
            // verifica se existe outro taxista com o mesmo Número de identificação
            var usrNum = (await _TaxistaRepository.Search(tx => tx.NumeroIdentificacao == summary.NumeroIdentificacao)).FirstOrDefault();
            if (usrNum != null && summary.NumeroIdentificacao != null)
            {
                AddNotification("Taxistas", string.Format("Número de identificação '{0}' está sendo utilizado por outro mototaxista", summary.NumeroIdentificacao));
            }

            if (IsInvalid())
            {
                return null;
            }

            return await base.CreateAsync(summary);
        }

        public async override Task<Taxista> UpdateAsync(TaxistaSummary summary)
        {
            var usrNum = (await _TaxistaRepository.Search(tx => tx.NumeroIdentificacao == summary.NumeroIdentificacao && tx.Id != summary.Id)).FirstOrDefault();
            if (usrNum != null && summary.NumeroIdentificacao != null)
            {
                AddNotification("Taxistas", string.Format("Número de identificação '{0}' está sendo utilizado por outro mototaxista", summary.NumeroIdentificacao));
            }

            if (IsInvalid())
            {
                return null;
            }

            return await base.UpdateAsync(summary);
        }

        protected override async Task<TaxistaSummary> CreateSummaryAsync(Taxista entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;

                var Taxista = new TaxistaSummary
                {
                    Id = entry.Id,
	                NumeroIdentificacao = entry.NumeroIdentificacao,
                    Ativo = entry.Ativo,
                    IdFoto = entry.IdFoto,
                    IdLocalizacaoAtual = entry.IdLocalizacaoAtual,
                    IdPontoTaxi = entry.IdPontoTaxi,
                    Disponivel = entry.Disponivel,
                };

                if (entry.Endereco != null)
                {
                    Taxista.Endereco = new EnderecoSummary()
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

                if (entry.Usuario != null)
                {
                    Taxista.Usuario = new UsuarioSummary()
                    {
                        Id = entry.Usuario.Id,
                        Nome = entry.Usuario.Nome,
                        Email = entry.Usuario.Email,
                        Telefone = entry.Usuario.PhoneNumber,
                        CPF = entry.Usuario.CPF,
                        RG = entry.Usuario.RG
                    };
                }

                return Taxista;
            });
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
            entry.Ativo = summary.Ativo;
            entry.NumeroIdentificacao = summary.NumeroIdentificacao;

            if (!entry.IdUsuario.HasValue || entry.IdUsuario.Value == Guid.Empty)
            {
                entry.IdUsuario = summary.Usuario.Id;
            }
            //entry.IdFoto = summary.IdFoto;
            //entry.IdLocalizacaoAtual = summary.IdLocalizacaoAtual;
            entry.IdPontoTaxi = summary.IdPontoTaxi;
            //entry.IdEndereco = summary.Endereco.Id;
        }

        protected override void ValidateSummary(TaxistaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Taxista: sumário é obrigatório"));
            }
        }

        /*public override async Task<Taxista> UpdateAsync(TaxistaSummary summary)
        {
            Guid oldFotoID = Guid.Empty;

            var entry = await GetRepository().FindByIdAsync(GetKeyFromSummary(summary));
            if (entry != null)
            {
                oldFotoID = entry.IdFoto ?? Guid.Empty;
            }

            var updatedEntry = await base.UpdateAsync(summary);

            if(updatedEntry != null && oldFotoID != Guid.Empty && oldFotoID != summary.IdFoto)
            {
                // remove permanentemente a foto anterior
                await _FotoService.DeleteAsync(oldFotoID, false);
            }

            return updatedEntry;
        }*/

        public override async Task<Taxista> Get(Guid key, string[] paths = null)
        {
            return await base.Get(key, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override async Task<IEnumerable<Taxista>> GetAll(string[] paths = null)
        {
            return await base.GetAll(paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override async Task<IEnumerable<Taxista>> Search(Expression<Func<Taxista, bool>> where, string[] paths = null, Pagination pagination = null)
        {
            return await base.Search(where, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths, pagination);
        }

        public async Task<TaxistaSummary> GetByUserId(Guid id)
        {
            var taxista = (await base.Search(x => x.IdUsuario == id, defaultPaths, null)).FirstOrDefault();
            if (taxista == null)
            {
                AddNotification(new Notification("Taxistas", "Taxista não encontrado."));
                return default;
            }

            return await CreateSummaryAsync(taxista);
        }

        public async Task<bool> MakeTaxistOnlineAsync(Guid id, bool disponivel)
        {
            bool sucesso = false;

            var taxista = await _TaxistaRepository.FindByIdAsync(id);

            if (!(await _veiculoTaxistaService.IsTaxiAtivoEmUsoPorOutroTaxista(id)) && disponivel && taxista.Ativo)
            {
                taxista.Disponivel = true;
            }
            else
            {
                taxista.Disponivel = false;
            }

            sucesso = (!(await _veiculoTaxistaService.IsTaxiAtivoEmUsoPorOutroTaxista(id)) && disponivel) || !disponivel;

            sucesso = sucesso && taxista.Ativo;

            if (sucesso)
                sucesso = await _TaxistaRepository.ModifyAsync(taxista);

            return sucesso;
        }

        public async Task<bool> InformarLocalizacao(Guid Key, LocalizacaoSummary localizacao)
        {
            var taxista = await Get(Key);
            if (taxista is null)
            {
                AddNotification(new Notification("Taxistas", "Informar localização: taxista não localizado"));
                return false;
            }

            var localizacaoSummmary = await _LocalizacaoService.GetSummaryAsync(taxista.LocalizacaoAtual);

            localizacaoSummmary.Latitude = localizacao.Latitude;
            localizacaoSummmary.Longitude = localizacao.Longitude;
            localizacaoSummmary.Angulo = localizacao.Angulo;
            localizacaoSummmary.IdUsuario = taxista.IdUsuario;

            var resultado = (await _LocalizacaoService.UpdateAsync(localizacaoSummmary)) != null;

            if (resultado)
            {
                await _proxyNotificacoesLocalizacao.InformarLocalizacaoTaxista(
                    taxista.Id,
                    Convert.ToDouble(localizacao.Latitude, CultureInfo.InvariantCulture.NumberFormat),
                    Convert.ToDouble(localizacao.Longitude, CultureInfo.InvariantCulture.NumberFormat),
                    localizacao.Angulo);
            }

            return resultado;
        }

        public async Task<int> ClassificacaoTaxista(Guid id)
        {
            var avaliacoesTaxista = await _corridaRepository.Search(
                x =>
                x.IdTaxista == id &&
                x.AvaliacaoTaxista != null && x.AvaliacaoTaxista != Enums.AvaliacaoUsuario.Indefinido);

            if (avaliacoesTaxista.Any())
            {
                return avaliacoesTaxista.Sum(x => (int)x.AvaliacaoTaxista) / avaliacoesTaxista.Count();
            }

            return 0;
        }

        public async Task<IEnumerable<Taxista>> ProcurarPorDistancia(LocalizacaoSummary origem, double? raio_minimo, double? raio_maximo, string[] paths = null)
        {
            return await ProcurarPorDistancia(new Localizacao()
            {
                Endereco = origem.Endereco,
                Id = origem.Id,
                IdUsuario = origem.IdUsuario,
                Latitude = origem.Latitude,
                Longitude = origem.Longitude,
                NomePublico = origem.NomePublico
            }, raio_minimo, raio_maximo, paths);
        }

        public async Task<IEnumerable<Taxista>> ProcurarPorDistancia(Localizacao origem, double? raio_minimo, double? raio_maximo, string[] paths = null)
        {
            raio_minimo = raio_minimo ?? 0;
            raio_maximo = raio_maximo ?? double.MaxValue;

            var builtinPaths = new[] { "LocalizacaoAtual", "Veiculos" };

            paths = paths != null ? paths.Union(builtinPaths).ToArray() : builtinPaths;

            var taxistas = await _TaxistaRepository.Search(
                taxista =>
                    taxista.Ativo && // taxista que está ativo
                    taxista.Disponivel && // ... que está disponível
                    taxista.Veiculos.Any(veicTx => veicTx.Ativo) // ... que está utilizando um veículo no momento
                    //DateTime.Now.AddSeconds(-20) <= taxista.LocalizacaoAtual.Updated
            , paths);

            var taxistas_com_distancias =
                from tx in taxistas
                select new
                {
                    taxista = tx,
                    distancia = Localizacao.ObterDistancia(origem, tx.LocalizacaoAtual)
                };

            var resultFinal = from tx in taxistas_com_distancias
                              where tx.distancia >= raio_minimo && tx.distancia <= raio_maximo
                              orderby tx.distancia
                              select tx.taxista;

            return resultFinal;
        }
    }
}
